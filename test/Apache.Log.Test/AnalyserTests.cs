using Apache.Log.AccessLog;
using Apache.Log.Configuration;
using Apache.Log.Resource;
using System;
using System.Collections.Generic;
using System.IO.Abstractions.TestingHelpers;
using System.Linq;
using Xunit;

namespace Apache.Log.Test
{
    public class AnalyserTests
    {
        [Fact]
        public void GetAllUnidentifiedResourceRequestsInLogFile_ShouldNotReturnAnything_FromLogFilesWithOnlyWhitelistedResources()
        {
            // Arrange
            var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
            {
                { @"c:\logs\website.com.access.2018.04.13.log", new MockFileData(
                    string.Join(Environment.NewLine, new List<string>()
                    {
                        @"117.34.118.109 - - [13/Apr/2018:03:55:08 +0100] ""GET /okBasePath/index.php HTTP/1.1"" 301 244",
                        @"117.34.118.109 - - [13/Apr/2018:03:55:09 +0100] ""GET /AnotherOKBasePath/index.php HTTP/1.1"" 301 245",
                        @"117.34.118.109 - - [13/Apr/2018:03:55:10 +0100] ""GET /okBasePath/subPath/index.php HTTP/1.1"" 301 244",
                    }))
                }
            });

            var accessLogParser = new Parser(fileSystem, AccessLogConfig.GetDefault());

            var whitelistedResources = new List<string>()
            {
                @"okBasePath",
                @"AnotherOKBasePath",
            }.AsQueryable();
            var whitelist = new Whitelist(whitelistedResources);

            var blacklistedResources = new List<string>().AsQueryable();
            var blacklist = new Blacklist(blacklistedResources);

            var identifier = new Analyser(accessLogParser, whitelist, blacklist, fileSystem);

            // Act
            var distinctUnidentifiedResources = identifier.GetAllUnidentifiedResourceRequestsInLogFile(@"c:\logs\website.com.access.2018.04.13.log");

            // Assert
            Assert.Empty(distinctUnidentifiedResources);
        }

        [Fact]
        public void GetAllUnidentifiedResourceRequestsInLogFile_ShouldNotReturnAnything_FromLogFilesWithOnlyBlacklistedResources()
        {
            // Arrange
            var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
            {
                { @"c:\logs\website.com.access.2018.04.13.log", new MockFileData(
                    string.Join(Environment.NewLine, new List<string>()
                    {
                        @"117.34.118.109 - - [13/Apr/2018:03:55:08 +0100] ""GET /pmamy/index.php HTTP/1.1"" 301 244",
                        @"117.34.118.109 - - [13/Apr/2018:03:55:09 +0100] ""GET /pmamy2/index.php HTTP/1.1"" 301 245",
                        @"117.34.118.109 - - [13/Apr/2018:03:55:10 +0100] ""GET /mysql/index.php HTTP/1.1"" 301 244",
                    }))
                }
            });

            var accessLogParser = new Parser(fileSystem, AccessLogConfig.GetDefault());

            var whitelistedResources = new List<string>().AsQueryable();
            var whitelist = new Whitelist(whitelistedResources);

            var blacklistedResources = new List<string>()
            {
                @"/pmamy/index.php",
                @"/pmamy2/index.php",
                @"/mysql/index.php"
            }.AsQueryable();

            var blacklist = new Blacklist(blacklistedResources);

            var identifier = new Analyser(accessLogParser, whitelist, blacklist, fileSystem);

            // Act
            var distinctUnidentifiedResources = identifier.GetAllUnidentifiedResourceRequestsInLogFile(@"c:\logs\website.com.access.2018.04.13.log");

            // Assert
            Assert.Empty(distinctUnidentifiedResources);
        }

        [Fact]
        public void GetAllUnidentifiedResourceRequestsInLogFile_ShouldReturnDistinctResources_FromLogFilesWithDuplicates()
        {
            // Arrange
            var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
            {
                { @"c:\logs\website.com.access.2018.04.13.log", new MockFileData(
                    string.Join(Environment.NewLine, new List<string>()
                    {
                        @"117.34.118.109 - - [13/Apr/2018:03:55:08 +0100] ""GET /pmamy/index.php HTTP/1.1"" 301 244",
                        @"117.34.118.109 - - [13/Apr/2018:03:55:09 +0100] ""GET /pmamy2/index.php HTTP/1.1"" 301 245",
                        @"117.34.118.109 - - [13/Apr/2018:03:55:10 +0100] ""GET /mysql/index.php HTTP/1.1"" 301 244",
                        @"117.34.118.109 - - [13/Apr/2018:03:55:10 +0100] ""GET /admin/index.php HTTP/1.1"" 301 244",
                        @"117.34.118.109 - - [13/Apr/2018:03:55:11 +0100] ""GET /admin/index.php HTTP/1.1"" 301 244",
                    }))
                }
            });

            var accessLogParser = new Parser(fileSystem, AccessLogConfig.GetDefault());

            var whitelistedResources = new List<string>().AsQueryable();
            var whitelist = new Whitelist(whitelistedResources);

            var blacklistedResources = new List<string>().AsQueryable();
            var blacklist = new Blacklist(blacklistedResources);

            var identifier = new Analyser(accessLogParser, whitelist, blacklist, fileSystem);

            // Act
            var distinctUnidentifiedResources = identifier.GetAllUnidentifiedResourceRequestsInLogFile(@"c:\logs\website.com.access.2018.04.13.log");

            // Assert
            Assert.Collection(distinctUnidentifiedResources,
                resource => Assert.Contains("/pmamy/index.php", resource),
                resource => Assert.Contains("/pmamy2/index.php", resource),
                resource => Assert.Contains("/mysql/index.php", resource), 
                resource => Assert.Contains("/admin/index.php", resource));
        }
    }
}
