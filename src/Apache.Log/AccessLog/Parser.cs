using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO.Abstractions;
using System.Text.RegularExpressions;
using Apache.Log.Configuration;
using Apache.Log.Models;

namespace Apache.Log.AccessLog
{
    public interface IParser
    {
        bool Parse(string line, out AccessRequest accessRequest);
        IEnumerable<AccessRequest> Parse(string filename);
    }

    public class Parser : IParser
    {
        private readonly IFileSystem _fileSystem;
        private readonly AccessLogConfig _accessLogConfig;

        public Parser(IFileSystem fileSystem, AccessLogConfig accessLogConfig)
        {
            _fileSystem = fileSystem;
            _accessLogConfig = accessLogConfig;
        }

        public bool Parse(string line, out AccessRequest accessRequest)
        {
            var pattern = _accessLogConfig.Pattern;

            var m = Regex.Match(line, pattern, RegexOptions.IgnoreCase);

            accessRequest = new AccessRequest();

            if (m.Success)
            {
                accessRequest.IPAddress = m.Groups[_accessLogConfig.IPAddressPatternGroup].Value;

                accessRequest.DateTime = DateTime.ParseExact(
                    m.Groups[_accessLogConfig.DateTimePatternGroup].Value,
                    _accessLogConfig.DateTimeFormat,
                    CultureInfo.InvariantCulture);

                accessRequest.Method = m.Groups[_accessLogConfig.MethodPatternGroup].Value;
                accessRequest.Resource = m.Groups[_accessLogConfig.ResourcePatternGroup].Value;
                accessRequest.Protocol = m.Groups[_accessLogConfig.ProtocolPatternGroup].Value;
                accessRequest.StatusCode = int.Parse(m.Groups[_accessLogConfig.StatusCodePatternGroup].Value);
                accessRequest.Size = int.Parse(m.Groups[_accessLogConfig.SizePatternGroup].Value);
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
