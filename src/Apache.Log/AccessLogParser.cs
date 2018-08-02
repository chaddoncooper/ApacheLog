using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO.Abstractions;
using System.Text.RegularExpressions;
using Apache.Log.Models;

namespace Apache.Log
{
    public class AccessLogParser : IAccessLogParser
    {
        private readonly IFileSystem _fileSystem;

        public AccessLogParser(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public bool Parse(string line, out AccessRequest accessRequest)
        {
            var pattern = @"(.+)\s-\s-\s\[(.+)]\s""(GET|POST|PUT|DELETE)\s(\S+)\s(\S+)""\s(\d+)\s(\d+)";

            var m = Regex.Match(line, pattern, RegexOptions.IgnoreCase);

            accessRequest = new AccessRequest();

            if (m.Success)
            {
                accessRequest.IPAddress = m.Groups[1].Value;

                accessRequest.DateTime = DateTime.ParseExact(
                    m.Groups[2].Value, 
                    "dd/MMM/yyyy:HH:mm:ss zzz", 
                    CultureInfo.InvariantCulture);

                accessRequest.Method = m.Groups[3].Value;
                accessRequest.Resource = m.Groups[4].Value;
                accessRequest.Protocol = m.Groups[5].Value;
                accessRequest.StatusCode = int.Parse(m.Groups[6].Value);
                accessRequest.Size = int.Parse(m.Groups[7].Value);
            }

            return m.Success;
        }

        public IEnumerable<AccessRequest> Parse(string fileName)
        {
            var accessRequests = new List<AccessRequest>();
            foreach (var line in _fileSystem.File.ReadLines(fileName))
            {
                var accessRequest = new AccessRequest();
                var result = Parse(line, out accessRequest);
                if (result)
                {
                    accessRequests.Add(accessRequest);
                }
            }
            return accessRequests;
        }
    }
}
