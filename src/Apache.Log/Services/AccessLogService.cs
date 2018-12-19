using Apache.Log.AccessLog;
using Apache.Log.Configuration;
using Apache.Log.Data;
using System;
using System.Collections.Generic;

namespace Apache.Log
{
    public interface IAccessLogService
    {
        IEnumerable<string> GetAllUnidentifiedResourceRequestsSince(DateTime startDateTime, string path);
    }

    public class AccessLogService : IAccessLogService
    {
        private readonly ApacheLogContext _context;
        private readonly AccessLogConfig _accessLogConfig;
        private readonly IFinder _accessLogFinder;
        private readonly IAnalyser _accessLogAnalyser;

        public AccessLogService(ApacheLogContext context, AccessLogConfig accessLogConfig, IFinder accessLogFinder, IAnalyser accessLogAnalyser)
        {
            _context = context;
            _accessLogConfig = accessLogConfig;
            _accessLogFinder = accessLogFinder;
            _accessLogAnalyser = accessLogAnalyser;
        }

        public IEnumerable<string> GetAllUnidentifiedResourceRequestsSince(DateTime startDateTime, string path)
        {
            var files = _accessLogFinder.GetLogFilesCreatedOnOrAfter(startDateTime, path, _accessLogConfig);
            var unidentifiedResourceRequests = new List<string>();

            foreach (var file in files)
            {
                _accessLogAnalyser.GetAllUnidentifiedResourceRequestsInLogFile(file);
            }

            
            return unidentifiedResourceRequests;
        }
    }
}
