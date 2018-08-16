using Apache.Log.Models;
using Apache.Log.Resource;
using System.Collections.Generic;
using System.IO.Abstractions;

namespace Apache.Log.AccessLog
{
    public interface IAnalyser
    {
        IEnumerable<string> GetAllUnidentifiedResourceRequestsInLogFile(string path);
    }

    public class Analyser : IAnalyser
    {
        private readonly IParser _accessLogParser;
        private readonly IWhitelist _whitelist;
        private readonly IBlacklist _blacklist;
        private readonly IFileSystem _fileSystem;

        public Analyser(IParser accessLogParser, IWhitelist whitelist, IBlacklist blacklist, IFileSystem fileSystem)
        {
            _accessLogParser = accessLogParser;
            _whitelist = whitelist;
            _blacklist = blacklist;
            _fileSystem = fileSystem;
        }

        public IEnumerable<string> GetAllUnidentifiedResourceRequestsInLogFile(string filePath)
        {
            var unidentifiedResourceRequests = new List<string>();

            foreach (var line in _fileSystem.File.ReadAllLines(filePath))
            {
                var accessRequest = new AccessRequest();
                if (_accessLogParser.Parse(line, out accessRequest))
                {
                    if (!unidentifiedResourceRequests.Contains(accessRequest.Resource)
                        && !_whitelist.RequestedResourceIsWhitelisted(accessRequest)
                        && !_blacklist.RequestedResourceIsBlacklisted(accessRequest))
                    {
                        unidentifiedResourceRequests.Add(accessRequest.Resource);
                    }
                }
            }

            return unidentifiedResourceRequests;
        }
    }
}
