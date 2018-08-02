using System;

namespace Apache.Log.Models
{
    public class AccessRequest
    {
        public string IPAddress { get; set; }
        public DateTime DateTime { get; set; }
        public string Method { get; set; }
        public string Resource { get; set; }
        public string Protocol { get; set; }
        public int StatusCode { get; set; }
        public int Size { get; set; }
    }
}
