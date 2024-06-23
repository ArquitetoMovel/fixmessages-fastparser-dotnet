using System.Diagnostics.Metrics;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Running;

namespace FixFastParser
{
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

           BenchmarkRunner.Run<FixParseBenchmark>(config);
        }
    }
}
