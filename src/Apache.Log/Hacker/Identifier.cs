using Apache.Log.AccessLog;
using Apache.Log.Models;
using Apache.Log.Resource;
using System.Collections.Generic;
using System.IO.Abstractions;

namespace Apache.Log.Hacker
{
    public interface IIdentifier
    {
        IEnumerable<string> GetAllUnidentifiedResourceRequestsInLogFile(string path);
    }

    public class Identifier : IIdentifier
    {
        private readonly IParser _accessLogParser;
        private readonly Whitelist _whitelist;
        private readonly Blacklist _blacklist;
        private readonly IFileSystem _fileSystem;

        public Identifier(IParser accessLogParser, Whitelist whitelist, Blacklist blacklist, IFileSystem fileSystem)
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