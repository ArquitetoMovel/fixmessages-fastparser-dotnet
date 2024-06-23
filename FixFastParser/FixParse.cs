
using FixParser;

namespace FixFastParser;

public class FixParse(IFixMessageParser parser)
{
    private readonly IFixMessageParser _parser = parser;
    // measure this method with Benchmark.NET library
    public ExecutionReport ParseMessage(string message) =>
        _parser.Parse(message);
}
