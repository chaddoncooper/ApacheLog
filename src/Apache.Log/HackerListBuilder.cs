using Apache.Log.Models;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;

namespace Apache.Log
{
    public interface IHackerListBuilder
    {
        IEnumerable<string> GetAllUnidentifiedResourceRequestsInLogFile(string logFilePath);
    }

    public class HackerListBuilder : IHackerListBuilder
    {
        private readonly IAccessLogParser _accessLogParser;
        private readonly IHackerIdentifier _hackerIdentifier;
        private readonly IFileSystem _fileSystem;

        public HackerListBuilder(IAccessLogParser accessLogParser, IHackerIdentifier hackerIdentifier, IFileSystem fileSystem)
        {
            _accessLogParser = accessLogParser;
            _hackerIdentifier = hackerIdentifier;
            _fileSystem = fileSystem;
        }

        public IEnumerable<string> GetAllUnidentifiedResourceRequestsInLogFile(string logFilePath)
        {
            var files = _fileSystem.Directory.EnumerateFiles(logFilePath, "*.log");

            var unidentifiedResourceRequests = new List<string>();

            foreach (var file in files)
            {
                foreach (var line in _fileSystem.File.ReadAllLines(file))
                {
                    var accessRequest = new AccessRequest();
                    if (_accessLogParser.Parse(line, out accessRequest))
                    {
                        if (!_hackerIdentifier.RequestedResourceIsWhitelisted(accessRequest)
                            && !_hackerIdentifier.RequestedResourceIsKnownTarget(accessRequest))
                        {
                            unidentifiedResourceRequests.Add(accessRequest.Resource);
                        }
                    }
                }
            }
            return unidentifiedResourceRequests.Distinct();
        }
    }
}
