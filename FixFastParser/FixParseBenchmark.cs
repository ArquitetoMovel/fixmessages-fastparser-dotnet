using BenchmarkDotNet.Attributes;
using FixParser;

namespace FixFastParser;

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

