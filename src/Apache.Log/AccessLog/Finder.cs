using Apache.Log.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO.Abstractions;
using System.Text.RegularExpressions;

namespace Apache.Log.AccessLog
{
    public interface IFinder
    {
        IEnumerable<string> GetLogFilesCreatedOnOrAfter(DateTime givenDate, string path);
    }

    public class Finder : IFinder
    {
        private readonly IFileSystem _fileSystem;
        private readonly AccessLogConfig _accessLogConfig;

        public Finder(IFileSystem fileSystem, AccessLogConfig accessLogConfig)
        {
            _fileSystem = fileSystem;
            _accessLogConfig = accessLogConfig;
        }

        public IEnumerable<string> GetLogFilesCreatedOnOrAfter(DateTime givenDate, string path)
        {
            var logFiles = new List<string>();

            var pattern = _accessLogConfig.FilenamePattern;

            foreach (var file in _fileSystem.Directory.EnumerateFiles(path))
            {
                var match = Regex.Match(file, pattern, RegexOptions.IgnoreCase);

                if (match.Success)
                {
                    var fileCreatedOn = DateTime.ParseExact(
                    match.Groups[_accessLogConfig.FilenameDateGroup].Value,
                    _accessLogConfig.FilenameDateFormat,
                    CultureInfo.InvariantCulture
                    );

                    if (fileCreatedOn >= givenDate)
                        logFiles.Add(file);
                }
            }

            return logFiles;
        }
    }
}
