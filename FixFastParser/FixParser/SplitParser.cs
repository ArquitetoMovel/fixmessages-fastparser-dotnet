namespace FixFastParser;

public class SplitParser : IFixMessageParser
{
    public ExecutionReport Parse(string message)
    {
       var result = new ExecutionReport();
       string[] tags = message.Split('|');
        foreach (string tag in tags)
        {
            string[] keyValue = tag.Split('=');
            if (keyValue.Length == 2)
            {
                string tagNumber = keyValue[0];
                string tagValue = keyValue[1];

                switch (tagNumber)
                {
                    case "8":
                        result.BeginString = tagValue;
                        break;
                    case "9":
                        result.BodyLength = int.Parse(tagValue);
                        break;
                    case "35":
                        result.MsgType = tagValue;
                        break;
                    case "49":
                        result.SenderCompID = tagValue;
                        break;
                    case "56":
                        result.TargetCompID = tagValue;
                        break;
                    case "34":
                        result.MsgSeqNum = int.Parse(tagValue);
                        break;
                    case "52":
                        result.SendingTime = DateTime.ParseExact(tagValue, "yyyyMMdd-HH:mm:ss", null);
                        break;
                    case "11":
                        result.ClOrdID = tagValue;
                        break;
                    case "6":
                        result.AvgPx = decimal.Parse(tagValue);
                        break;
                    case "14":
                        result.CumQty = int.Parse(tagValue);
                        break;
                    case "17":
                        result.ExecID = tagValue;
                        break;
                    case "39":
                        result.OrdStatus = int.Parse(tagValue);
                        break;
                    case "55":
                        result.Symbol = tagValue;
                        break;
                    case "54":
                        result.Side = int.Parse(tagValue);
                        break;
                    case "37":
                        result.OrderID = tagValue;
                        break;
                    case "32":
                        result.LastShares = int.Parse(tagValue);
                        break;
                    case "31":
                        result.LastPx = decimal.Parse(tagValue);
                        break;
                    case "150":
                        result.ExecType = int.Parse(tagValue);
                        break;
                    case "151":
                        result.LeavesQty = int.Parse(tagValue);
                        break;
                    case "60":
                        result.TransactTime = DateTime.ParseExact(tagValue, "yyyyMMdd-HH:mm:ss", null);
                        break;
                    case "58":
                        result.Text = tagValue;
                        break;
                    default:
                        // Lidar com tags adicionais, se necessário
                        break;
                }
            }
        } 
        return result;
    }
}
