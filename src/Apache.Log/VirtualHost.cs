using System.Collections.Generic;

namespace Apache.Log.Configuration
{
    public class VirtualHost
    {
        public string Name { get; set; }
        public IEnumerable<AccessLogFile> AccessLogFiles { get; set; }
    }
}
