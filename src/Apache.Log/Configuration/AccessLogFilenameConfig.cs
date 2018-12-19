namespace Apache.Log.Configuration
{
    public class AccessLogFilenameConfig
    {
        public string FilenamePattern { get; set; }
        public int FilenameDateGroup { get; set; } = 2;
        public string FilenameDateFormat { get; set; }

        public static AccessLogFilenameConfig GetDefault()
        {
            return new AccessLogFilenameConfig()
            {
                FilenamePattern = @"(.+).access.(\d{4}.\d{2}.\d{2}).log",
                FilenameDateGroup = 2,
                FilenameDateFormat = "yyyy.MM.dd"
            };
        }
    }
}
