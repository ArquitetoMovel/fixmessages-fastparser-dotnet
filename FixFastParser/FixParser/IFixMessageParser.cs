using FixFastParser;

namespace FixParser;

public interface IFixMessageParser
{
    public ExecutionReport Parse(string message);
}
