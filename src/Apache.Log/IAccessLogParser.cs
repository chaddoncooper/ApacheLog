using Apache.Log.Models;
using System;
using System.Collections.Generic;

namespace Apache.Log
{
    public interface IAccessLogParser
    {
        bool Parse(string line, out AccessRequest accessRequest);
        IEnumerable<AccessRequest> Parse(string filename);
        IEnumerable<string> GetLogFilesCreatedOnOrAfter(DateTime date);
    }
}
