using System.Diagnostics.Metrics;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Loggers;
using OpenTelemetry;
using OpenTelemetry.Metrics;

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
            _parseMessageCounter.Add(1, 
                        new KeyValuePair<string, object?>("Id", Guid.NewGuid().ToString()));
            _fixParse.ParseMessage(_fixMessage);
        }

        [Benchmark]
        public void ParseMessageFaster()
        {
            _parseMessageFasterCounter.Add(1, 
                         new KeyValuePair<string, object?>("Id", Guid.NewGuid().ToString()));
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
            
            using var meterProvider = Sdk.CreateMeterProviderBuilder()
              .AddMeter("FixFastParser")
              .SetMaxMetricPointsPerMetricStream(80_000)
              
              //.AddConsoleExporter()
              .AddOtlpExporter((otlpExporter, metricsOptions) => 
              {
                otlpExporter.Endpoint = new Uri("http://localhost:4317"); // Replace with your OTLP collector endpoint  
                metricsOptions.PeriodicExportingMetricReaderOptions.ExportIntervalMilliseconds = 2_000;
                metricsOptions.TemporalityPreference = MetricReaderTemporalityPreference.Delta; ///
              })
              .Build();

            var client = new FixParseBenchmark();
            client.UpdateQueue(0);
            client.UpdateQueue(5);
            Thread.Sleep(6_000);
            client.UpdateQueue(30);
            Thread.Sleep(6_000);
            client.UpdateQueue(22);
            Thread.Sleep(6_000);
            client.UpdateQueue(10);
            Thread.Sleep(6_000);
            client.UpdateQueue(0);
            client.UpdateQueue(2);

    
            // var decrement = 150;
            // for (int i = 0; i < 300; i++)
            // {
            //     if (i <= 150)
            //     {

            //        client.UpdateQueue(i); 
            //        System.Console.WriteLine($"Increment the queue {i}");
            //     }else{
            //       client.UpdateQueue(decrement--);
            //       System.Console.WriteLine($"Decrement the queue {decrement}");
            //     }
            //     Thread.Sleep(2_000);

            // }
             //Parallel.For(0, 100_000, async i =>
             //{
               //for (int i = 0; i < 30_000; i++)
               //{
               //    client.ParseMessageFaster();
               //    //client.ParseMessage();
               //    if((i % 10_000) == 0) {
               //        Console.WriteLine($" =====> delayed message {i} =====> {DateTime.Now}");
               //        Task.Delay(30_000).Wait();
               //    }

               //   // if((i % 5000) == 0) {
               //    //    Console.WriteLine($" =====> block send {i} =====> {DateTime.Now}");
               //     //   Task.Delay(8_000).Wait();
               //   // }


               //}
                //System.Console.WriteLine($"Clien iterating {i}");
             //});       
             //Aguard   
          //while( Console.ReadKey().Key != ConsoleKey.G) {
          // Task.Delay(16_000).Wait();
          // System.Console.WriteLine("waiting for terminal");
          // //client.ReleaseMetrics();
          //// BenchmarkRunner.Run<FixParseBenchmark>(config);
          //}
        }
    }
}
