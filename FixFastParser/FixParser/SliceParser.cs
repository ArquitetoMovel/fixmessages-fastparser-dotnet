namespace FixFastParser;

public class SliceParser : IFixMessageParser
{
    public ExecutionReport Parse(string message)
    {
        var result = new ExecutionReport();
        ReadOnlySpan<char> fixSpan = message.AsSpan();

        while (!fixSpan.IsEmpty)
        {
            int delimiterIndex = fixSpan.IndexOf('=');
            if (delimiterIndex == -1)
                break;

            ReadOnlySpan<char> tagNumber = fixSpan[..delimiterIndex];
            fixSpan = fixSpan.Slice(delimiterIndex + 1);

            int valueDelimiterIndex = fixSpan.IndexOf('|');
            ReadOnlySpan<char> tagValue = fixSpan[..valueDelimiterIndex];
            fixSpan = fixSpan.Slice(valueDelimiterIndex + 1);

            ProcessTag(tagNumber, tagValue, result);
       }
       return result;
    }

private static void ProcessTag(ReadOnlySpan<char> tagNumber, ReadOnlySpan<char> tagValue, ExecutionReport executionReport)
{
    switch (tagNumber.ToString())
    {
        case "8":
            executionReport.BeginString = tagValue.ToString();
            break;
        case "9":
                _ = int.TryParse(tagValue, out int bodyLength);
            executionReport.BodyLength = bodyLength;
            break;
        case "35":
            executionReport.MsgType = tagValue.ToString();
            break;
        case "49":
            executionReport.SenderCompID = tagValue.ToString();
            break;
        case "56":
            executionReport.TargetCompID = tagValue.ToString();
            break;
        case "34":
                _ = int.TryParse(tagValue, out int msgSeqNum);
            executionReport.MsgSeqNum = msgSeqNum;
            break;
        case "52":
            executionReport.SendingTime = DateTime.ParseExact(tagValue.ToString(), "yyyyMMdd-HH:mm:ss", null);
            break;
        case "11":
            executionReport.ClOrdID = tagValue.ToString();
            break;
        case "6":
            decimal.TryParse(tagValue, out decimal avgPx);
            executionReport.AvgPx = avgPx;
            break;
        case "14":
            int.TryParse(tagValue, out int cumQty);
            executionReport.CumQty = cumQty;
            break;
        case "17":
            executionReport.ExecID = tagValue.ToString();
            break;
        case "39":
            int.TryParse(tagValue, out int ordStatus);
            executionReport.OrdStatus = ordStatus;
            break;
        case "55":
            executionReport.Symbol = tagValue.ToString();
            break;
        case "54":
                _ = int.TryParse(tagValue, out int side);
            executionReport.Side = side;
            break;
        case "37":
            executionReport.OrderID = tagValue.ToString();
            break;
        case "32":
                _ = int.TryParse(tagValue, out int lastShares);
            executionReport.LastShares = lastShares;
            break;
        case "31":
                _ = decimal.TryParse(tagValue, out decimal lastPx);
            executionReport.LastPx = lastPx;
            break;
        case "150":
                _ = int.TryParse(tagValue, out int execType);
            executionReport.ExecType = execType;
            break;
        case "151":
                _ = int.TryParse(tagValue, out int leavesQty);
            executionReport.LeavesQty = leavesQty;
            break;
        case "60":
            executionReport.TransactTime = DateTime.ParseExact(tagValue.ToString(), "yyyyMMdd-HH:mm:ss", null);
            break;
        case "58":
            executionReport.Text = tagValue.ToString();
            break;
        default:
            // Lidar com tags adicionais, se necessário
            break;
    }
}


}
