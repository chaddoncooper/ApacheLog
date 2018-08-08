namespace Apache.Log.Configuration
{
    public class SiteConfig
    {
        public string Name { get; set; }
        public string LogFilePath { get; set; }
        public AccessLogConfig AccessLogConfig { get; set; }
    }
}
