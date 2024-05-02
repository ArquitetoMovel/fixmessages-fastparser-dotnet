namespace FixFastParser;

public class ExecutionReport
{
    public string? BeginString { get; set; } // 8
    public int BodyLength { get; set; } // 9
    public string? MsgType { get; set; } // 35
    public string? SenderCompID { get; set; } // 49
    public string? TargetCompID { get; set; } // 56
    public int MsgSeqNum { get; set; } // 34
    public DateTime SendingTime { get; set; } // 52
    public string? ClOrdID { get; set; } // 11
    public decimal AvgPx { get; set; } // 6
    public int CumQty { get; set; } // 14
    public string? ExecID { get; set; } // 17
    public int OrdStatus { get; set; } // 39
    public string? Symbol { get; set; } // 55
    public int Side { get; set; } // 54
    public string? OrderID { get; set; } // 37
    public int LastShares { get; set; } // 32
    public decimal LastPx { get; set; } // 31
    public int ExecType { get; set; } // 150
    public int LeavesQty { get; set; } // 151
    public DateTime TransactTime { get; set; } // 60
    public string? Text { get; set; } // 58

    // Método para gerar a mensagem FIX
    public string ToFIXString()
    {
        return $"8={BeginString}|9={BodyLength}|35={MsgType}|49={SenderCompID}|56={TargetCompID}|34={MsgSeqNum}|52={SendingTime.ToString("yyyyMMdd-HH:mm:ss")}|11={ClOrdID}|6={AvgPx}|14={CumQty}|17={ExecID}|39={OrdStatus}|55={Symbol}|54={Side}|37={OrderID}|32={LastShares}|31={LastPx}|150={ExecType}|151={LeavesQty}|60={TransactTime.ToString("yyyyMMdd-HH:mm:ss")}|58={Text}|";
    }
}
