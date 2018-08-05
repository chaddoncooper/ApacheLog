public class AccessRequetPatternConfig
{
    public string Pattern { get; set; } = @"(.+)\s-\s-\s\[(.+)]\s""(GET|POST|PUT|DELETE)\s(\S+)\s(\S+)""\s(\d+)\s(\d+)";
    public int IPAddressPatternGroup { get; set; } = 1;
    public int DateTimePatternGroup { get; set; } = 2;
    public string DateTimeFormat { get; set; } = "dd/MMM/yyyy:HH:mm:ss zzz";
    public int MethodPatternGroup { get; set; } = 3;
    public int ResourcePatternGroup { get; set; } = 4;
    public int ProtocolPatternGroup { get; set; } = 5;
    public int StatusCodePatternGroup { get; set; } = 6;
    public int SizePatternGroup { get; set; } = 7;
}