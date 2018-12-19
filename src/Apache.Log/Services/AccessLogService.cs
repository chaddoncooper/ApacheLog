using Apache.Log.AccessLog;
using Apache.Log.Configuration;
using Apache.Log.Data;
using System;
using System.Collections.Generic;

namespace Apache.Log
{
    public interface IAccessLogService
    {
        IEnumerable<string> GetAllUnidentifiedResourceRequestsSince(VirtualHost virtualHost, DateTime startDateTime, string path);
    }

    public class AccessLogService : IAccessLogService
    {
        private readonly ApacheLogContext _context;
        private readonly IFinder _accessLogFinder;
        private readonly IAnalyser _accessLogAnalyser;

        public AccessLogService(ApacheLogContext context, IFinder accessLogFinder, IAnalyser accessLogAnalyser)
        {
            _context = context;
            _accessLogFinder = accessLogFinder;
            _accessLogAnalyser = accessLogAnalyser;
        }

        public IEnumerable<string> GetAllUnidentifiedResourceRequestsSince(VirtualHost virtualHost, DateTime startDateTime, string path)
        {
            var logFilePaths = new List<string>();
                
            foreach (var accessLog in virtualHost.AccessLogFiles)
            {
                logFilePaths.AddRange(_accessLogFinder.GetLogFilesCreatedOnOrAfter(startDateTime, path, accessLog.AccessLogFilenameConfig));
            }
                
            var unidentifiedResourceRequests = new List<string>();

            foreach (var file in logFilePaths)
            {
                _accessLogAnalyser.GetAllUnidentifiedResourceRequestsInLogFile(file);
            }

            
            return unidentifiedResourceRequests;
        }
    }
}
