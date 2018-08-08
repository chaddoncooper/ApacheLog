namespace Apache.Log.Configuration
{
    public class AccessLogConfig
    {
        public string Pattern { get; set; }
        public int IPAddressPatternGroup { get; set; }
        public int DateTimePatternGroup { get; set; }
        public string DateTimeFormat { get; set; }
        public int MethodPatternGroup { get; set; }
        public int ResourcePatternGroup { get; set; }
        public int ProtocolPatternGroup { get; set; }
        public int StatusCodePatternGroup { get; set; }
        public int SizePatternGroup { get; set; }
        public string FilenamePattern { get; set; }
        public int FilenameDateGroup { get; set; } = 2;
        public string FilenameDateFormat { get; set; }

        public static AccessLogConfig GetDefault()
        {
            return new AccessLogConfig()
            {
                Pattern = @"(.+)\s-\s-\s\[(.+)]\s""(GET|POST|PUT|DELETE)\s(\S+)\s(\S+)""\s(\d+)\s(\d+)",
                IPAddressPatternGroup = 1,
                DateTimePatternGroup = 2,
                DateTimeFormat = "dd/MMM/yyyy:HH:mm:ss zzz",
                MethodPatternGroup = 3,
                ResourcePatternGroup = 4,
                ProtocolPatternGroup = 5,
                StatusCodePatternGroup = 6,
                SizePatternGroup = 7,
                FilenamePattern = @"(.+).access.(\d{4}.\d{2}.\d{2}).log",
                FilenameDateGroup = 2,
                FilenameDateFormat = "yyyy.MM.dd"
            };
        }
    }
}