using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Running;


namespace FixFastParser
{
    public class FixParseBenchmark
    {
        private readonly FixParse _fixParse;
        private readonly string _fixMessage;

        public FixParseBenchmark()
        {
            var parser = new SliceParser(); // Replace with your actual implementation
            _fixParse = new FixParse(parser);
            _fixMessage = File.ReadAllText("FixMessage.txt");
        }

        [Benchmark]
        public void ParseMessage()
        {
            // read the content of #FixMessage.txt and create a variable
            // with the content of the file
            _fixParse.ParseMessage(_fixMessage);
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