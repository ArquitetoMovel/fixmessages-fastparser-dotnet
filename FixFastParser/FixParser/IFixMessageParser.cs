namespace FixFastParser;

public interface IFixMessageParser
{
    public ExecutionReport Parse(string message);
}
