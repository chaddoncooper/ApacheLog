using Apache.Log.AccessLog;
using Apache.Log.Configuration;
using Apache.Log.Data.Entities;
using Apache.Log.Resource;
using Apache.Log.Test.TestFactories;
using System;
using System.Collections.Generic;
using System.IO.Abstractions.TestingHelpers;
using System.Linq;
using Xunit;

namespace Apache.Log.Test
{
    public class AnalyserTests
    {
        private readonly IApacheLogContextFactory _apacheLogContextFactory;

        public AnalyserTests()
        {
            _apacheLogContextFactory = new ApacheLogContextFactory();
        }

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

            using (var context = _apacheLogContextFactory.NewTestContext())
            {
                context.WhitelistedResources.AddRange(
                    new WhitelistedResource[2]
                    {
                        new WhitelistedResource() { BasePath = "okBasePath" },
                        new WhitelistedResource() { BasePath = "AnotherOKBasePath" }
                    });
                context.SaveChanges();

                var whitelist = new Whitelist(context);
                var blacklist = new Blacklist(context);

                var identifier = new Analyser(accessLogParser, whitelist, blacklist, fileSystem);

                // Act
                var distinctUnidentifiedResources = identifier.GetAllUnidentifiedResourceRequestsInLogFile(@"c:\logs\website.com.access.2018.04.13.log");

                // Assert
                Assert.Empty(distinctUnidentifiedResources);
            } 
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

            using (var context = _apacheLogContextFactory.NewTestContext())
            {
                context.AddRange(
                    new BlacklistedResource[3] {
                        new BlacklistedResource() { FullPath = @"/pmamy/index.php" },
                        new BlacklistedResource() { FullPath = @"/pmamy2/index.php" },
                        new BlacklistedResource() { FullPath = @"/mysql/index.php" }
                    });
                context.SaveChanges();

                var whitelist = new Whitelist(context);
                var blacklist = new Blacklist(context);

                var identifier = new Analyser(accessLogParser, whitelist, blacklist, fileSystem);

                // Act
                var distinctUnidentifiedResources = identifier.GetAllUnidentifiedResourceRequestsInLogFile(@"c:\logs\website.com.access.2018.04.13.log");

                // Assert
                Assert.Empty(distinctUnidentifiedResources);
            }                
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

            // Run the test against one instance of the context
            using (var context = _apacheLogContextFactory.NewTestContext())
            {
                var whitelist = new Whitelist(context);
                var blacklist = new Blacklist(context);

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
}
