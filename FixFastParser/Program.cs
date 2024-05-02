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
    }

    public class Program
    {
        private static readonly ILogger _logger = new ConsoleLogger();
        public static void Main(string[] args)
        {
            _logger.WriteInfo("starting program");
            var config = new ManualConfig()
            .WithOptions(ConfigOptions.DisableOptimizationsValidator);
            //config.AddExporter();
            config.AddLogger(_logger);
            config.AddColumnProvider();
            BenchmarkRunner.Run<FixParseBenchmark>(config);
        }
    }
}