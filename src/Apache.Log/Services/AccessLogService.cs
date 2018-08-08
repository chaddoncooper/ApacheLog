using Apache.Log.AccessLog;
using Apache.Log.Hacker;
using System;
using System.Collections.Generic;
using System.IO.Abstractions;

namespace Apache.Log
{
    public interface IAccessLogService
    {
        IEnumerable<string> GetAllUnidentifiedResourceRequestsSince(DateTime startDateTime, string path);
    }

    public class AccessLogService : IAccessLogService
    {
        private readonly IFinder _accessLogFinder;
        private readonly IIdentifier _identifier;

        public AccessLogService(IFinder accessLogFinder, IIdentifier identifier)
        {
            _accessLogFinder = accessLogFinder;
            _identifier = identifier;
        }

        public IEnumerable<string> GetAllUnidentifiedResourceRequestsSince(DateTime startDateTime, string path)
        {
            var files = _accessLogFinder.GetLogFilesCreatedOnOrAfter(startDateTime, path);

            var unidentifiedResourceRequests = new List<string>();
            return unidentifiedResourceRequests;
        }
    }
}
