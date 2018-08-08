using Apache.Log.AccessLog;
using Apache.Log.Configuration;
using System;
using System.IO.Abstractions;

namespace Apache.Log
{
    class Program
    {
        static void Main(string[] args)
        {
            var fileSystem = new FileSystem();
            var accessLogParser = new Parser(fileSystem, new AccessLogConfig());

            foreach (var accessRequest in accessLogParser.Parse(args[0]))
            {
                Console.WriteLine(accessRequest.Resource);
            }

            Console.ReadKey();
        }
    }
}
