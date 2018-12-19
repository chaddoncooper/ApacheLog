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
        IEnumerable<string> GetLogFilesCreatedOnOrAfter(DateTime givenDate, string path, AccessLogConfig accessLogConfig);
    }

    public class Finder : IFinder
    {
        private readonly IFileSystem _fileSystem;

        public Finder(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public IEnumerable<string> GetLogFilesCreatedOnOrAfter(DateTime givenDate, string path, AccessLogConfig accessLogConfig)
        {
            var logFiles = new List<string>();

            foreach (var file in _fileSystem.Directory.EnumerateFiles(path))
            {
                var match = Regex.Match(file, accessLogConfig.FilenamePattern, RegexOptions.IgnoreCase);

                if (match.Success)
                {
                    var fileCreatedOn = DateTime.ParseExact(
                    match.Groups[accessLogConfig.FilenameDateGroup].Value,
                    accessLogConfig.FilenameDateFormat,
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
