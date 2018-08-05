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
        private readonly ApacheLogParserConfig _accessRequetPatternConfig;

        public AccessLogParser(IFileSystem fileSystem, ApacheLogParserConfig accessRequestPatternConfig)
        {
            _fileSystem = fileSystem;
            _accessRequetPatternConfig = accessRequestPatternConfig;
        }

        public IEnumerable<string> GetLogFilesCreatedOnOrAfter(DateTime givenDate, string path)
        {
            var logFiles = new List<string>();

            var pattern = _accessRequetPatternConfig.FilenamePattern;

            foreach (var file in _fileSystem.Directory.EnumerateFiles(path))
            {
                var match = Regex.Match(file, pattern, RegexOptions.IgnoreCase);

                if (match.Success)
                {
                    var fileCreatedOn = DateTime.ParseExact(
                    match.Groups[_accessRequetPatternConfig.FilenameDateGroup].Value,
                    _accessRequetPatternConfig.FilenameDateFormat,
                    CultureInfo.InvariantCulture
                    );

                    if (fileCreatedOn >= givenDate)
                        logFiles.Add(file);
                }
            }

            return logFiles;
        }

        public bool Parse(string line, out AccessRequest accessRequest)
        {
            var pattern = _accessRequetPatternConfig.Pattern;

            var m = Regex.Match(line, pattern, RegexOptions.IgnoreCase);

            accessRequest = new AccessRequest();

            if (m.Success)
            {
                accessRequest.IPAddress = m.Groups[_accessRequetPatternConfig.IPAddressPatternGroup].Value;

                accessRequest.DateTime = DateTime.ParseExact(
                    m.Groups[_accessRequetPatternConfig.DateTimePatternGroup].Value,
                    _accessRequetPatternConfig.DateTimeFormat,
                    CultureInfo.InvariantCulture);

                accessRequest.Method = m.Groups[_accessRequetPatternConfig.MethodPatternGroup].Value;
                accessRequest.Resource = m.Groups[_accessRequetPatternConfig.ResourcePatternGroup].Value;
                accessRequest.Protocol = m.Groups[_accessRequetPatternConfig.ProtocolPatternGroup].Value;
                accessRequest.StatusCode = int.Parse(m.Groups[_accessRequetPatternConfig.StatusCodePatternGroup].Value);
                accessRequest.Size = int.Parse(m.Groups[_accessRequetPatternConfig.SizePatternGroup].Value);
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
