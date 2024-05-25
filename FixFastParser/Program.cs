using System.Diagnostics.Metrics;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Running;

namespace FixFastParser
{
    public class FixParseBenchmark
    {
        private readonly FixParse _fixParse;
        private readonly FixParse _fixParseFasterVersion;
        private readonly string _fixMessage;
        private static readonly Meter _meter = new Meter("FixFastParser", "1.0.0");
        private static readonly Counter<int> _parseMessageCounter = _meter.CreateCounter<int>("parse_message_counter", "Count", "Count of parse_message calls");
        private static readonly Counter<int> _parseMessageFasterCounter = _meter.CreateCounter<int>("parse_message_faster_counter", "Count" , "Count of parse_message_faster calls");
        private static readonly UpDownCounter<long> _upQueueTestCounter = _meter.CreateUpDownCounter<long>("up_queue_tst_counter", "Count", "Size current of the queue");


        public FixParseBenchmark()
        {
            _fixParse = new FixParse(new SplitParser());
            _fixParseFasterVersion = new FixParse(new SliceParser());
            _fixMessage = File.ReadAllText("FixMessage.txt");
 
        }

        [Benchmark]
        public void ParseMessage()
        {
            _fixParse.ParseMessage(_fixMessage);
        }

        [Benchmark]
        public void ParseMessageFaster()
        {
            _fixParseFasterVersion.ParseMessage(_fixMessage);
            
        }

        public void UpdateQueue(long actualValue){
          _upQueueTestCounter.Add(actualValue, 
              new KeyValuePair<string, object?>("registrationtime", DateTime.Now.ToString("ddmmyyhhmm-ss")));
        }

        public void ReleaseMetrics() {
            _meter.Dispose();
        }
    }

    public class Program
    {
        private static readonly ILogger _logger = new ConsoleLogger();
        public static void Main(string[] args)
        {
           var config = new ManualConfig()
           .WithOptions(ConfigOptions.DisableOptimizationsValidator);
            //config.
            config.AddLogger(_logger);
            config.AddColumnProvider();
            
           //using var meterProvider = Sdk.CreateMeterProviderBuilder()
           //  .AddMeter("FixFastParser")
           //  .SetMaxMetricPointsPerMetricStream(80_000)
           //  
           //  //.AddConsoleExporter()
           //  .AddOtlpExporter((otlpExporter, metricsOptions) => 
           //  {
           //    otlpExporter.Endpoint = new Uri("http://localhost:4317"); // Replace with your OTLP collector endpoint  
           //    metricsOptions.PeriodicExportingMetricReaderOptions.ExportIntervalMilliseconds = 2_000;
           //    metricsOptions.TemporalityPreference = MetricReaderTemporalityPreference.Delta; ///
           //  })
           //  .Build();

           BenchmarkRunner.Run<FixParseBenchmark>(config);
        }
    }
}
