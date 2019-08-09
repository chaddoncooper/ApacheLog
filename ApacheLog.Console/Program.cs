using Apache.Log.AccessLog;
using Apache.Log.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using System.Text;

namespace ApacheLog.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var parserConfig = AccessLogParserConfig.GetDefault();
            var fileSystem = new FileSystem();


            var parser = new Parser(fileSystem, parserConfig);

            var accessRequests = parser.Parse(args[0]);

            var ips = accessRequests.Select(req => req.IPAddress);
            File.WriteAllLines(args[1], ips);
        }
    }
}
