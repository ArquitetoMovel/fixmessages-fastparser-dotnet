namespace FixFastParser;

public class FixParse
{
    private readonly IFixMessageParser _parser;

    public FixParse(IFixMessageParser parser)
    {
       _parser = parser;
    }
    // measure this method with Benchmark.NET library
    public ExecutionReport ParseMessage(string message) =>
        _parser.Parse(message);
}
