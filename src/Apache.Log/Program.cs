using System;
using System.IO.Abstractions;

namespace Apache.Log
{
    class Program
    {
        static void Main(string[] args)
        {
            var fileSystem = new FileSystem();
            var accessLogParser = new AccessLogParser(fileSystem, new AccessRequetPatternConfig());

            foreach (var accessRequest in accessLogParser.Parse(args[0]))
            {
                Console.WriteLine(accessRequest.Resource);
            }

            Console.ReadKey();
        }
    }
}
