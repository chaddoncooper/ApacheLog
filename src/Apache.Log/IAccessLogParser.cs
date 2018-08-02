using Apache.Log.Models;
using System.Collections.Generic;

namespace Apache.Log
{
    public interface IAccessLogParser
    {
        bool Parse(string line, out AccessRequest accessRequest);
        IEnumerable<AccessRequest> Parse(string filename);
    }
}
