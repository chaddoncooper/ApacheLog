namespace Apache.Log.Configuration
{
    public class AccessLogParserConfig
    {
        public string Pattern { get; set; }
        public int IPAddressPatternGroup { get; set; }
        public int DateTimePatternGroup { get; set; }
        public int MethodPatternGroup { get; set; }
        public int ResourcePatternGroup { get; set; }
        public int ProtocolPatternGroup { get; set; }
        public int StatusCodePatternGroup { get; set; }
        public int SizePatternGroup { get; set; }
        public string DateTimeFormat { get; set; }

        public static AccessLogParserConfig GetDefault()
        {
            return new AccessLogParserConfig()
            {
                Pattern = @"(.+)\s-\s-\s\[(.+)]\s""(GET|POST|PUT|DELETE)\s(\S+)\s(\S+)""\s(\d+)\s(\d+)",
                IPAddressPatternGroup = 1,
                DateTimePatternGroup = 2,
                MethodPatternGroup = 3,
                ResourcePatternGroup = 4,
                ProtocolPatternGroup = 5,
                StatusCodePatternGroup = 6,
                SizePatternGroup = 7,
                DateTimeFormat = "dd/MMM/yyyy:HH:mm:ss zzz"
            };
        }
    }
}
