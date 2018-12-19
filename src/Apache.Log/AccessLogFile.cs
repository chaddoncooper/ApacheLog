using Apache.Log.Configuration;

namespace Apache.Log
{
    public class AccessLogFile
    {
        public string DirectoryPath { get; }
        public AccessLogFilenameConfig AccessLogFilenameConfig { get; }
        public AccessLogParserConfig AccessLogParserConfig { get; }
        public bool SSL { get; set; }

        public AccessLogFile(string directoryPath, AccessLogFilenameConfig accessLogFilenameConfig, AccessLogParserConfig accessLogParserConfig, bool ssl)
        {
            DirectoryPath = directoryPath;
            AccessLogFilenameConfig = accessLogFilenameConfig;
            AccessLogParserConfig = accessLogParserConfig;
            SSL = ssl;
        }
    }
}
