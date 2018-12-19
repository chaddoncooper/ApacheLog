using Apache.Log.AccessLog;
using Apache.Log.Configuration;
using System;
using System.Collections.Generic;
using System.IO.Abstractions.TestingHelpers;
using Xunit;

namespace Apache.Log.Test
{
    public class AccessLogFinderTests
    {
        [Fact]
        public void GetLogFilesCreatedOnOrAfter_ReturnsAnEnumerationOfFiles_CreatedOnOrAfterDate()
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
                    }))
                },
                { @"c:\logs\website.com.access.2018.04.14.log", new MockFileData(
                    string.Join(Environment.NewLine, new List<string>()
                    {
                        @"117.34.118.110 - - [14/Apr/2018:04:55:08 +0100] ""GET /pmamy/index.php HTTP/1.1"" 301 244",
                        @"117.34.118.110 - - [14/Apr/2018:04:55:09 +0100] ""GET /pmamy2/index.php HTTP/1.1"" 301 245",
                        @"117.34.118.110 - - [14/Apr/2018:04:55:10 +0100] ""GET /mysql/index.php HTTP/1.1"" 301 244",
                        @"117.34.118.110 - - [14/Apr/2018:04:55:10 +0100] ""GET /admin/index.php HTTP/1.1"" 301 244",
                    }))
                },
                { @"c:\logs\website.com.access.2018.04.15.log", new MockFileData(
                    string.Join(Environment.NewLine, new List<string>()
                    {
                        @"117.34.118.111 - - [15/Apr/2018:05:55:08 +0100] ""GET /pmamy/index.php HTTP/1.1"" 301 244",
                        @"117.34.118.111 - - [15/Apr/2018:05:55:09 +0100] ""GET /pmamy2/index.php HTTP/1.1"" 301 245",
                        @"117.34.118.111 - - [15/Apr/2018:05:55:10 +0100] ""GET /mysql/index.php HTTP/1.1"" 301 244",
                        @"117.34.118.111 - - [15/Apr/2018:05:55:10 +0100] ""GET /admin/index.php HTTP/1.1"" 301 244",
                    }))
                }
            });

            var accessLogFinder = new Finder(fileSystem);

            var accessLogConfig = AccessLogConfig.GetDefault();

            // Act
            var logFiles = accessLogFinder.GetLogFilesCreatedOnOrAfter(new DateTime(2018, 4, 14), @"c:\logs\", accessLogConfig);

            // Assert
            Assert.Collection(logFiles, item => Assert.Contains(@"c:\logs\website.com.access.2018.04.14.log", item),
                    item => Assert.Contains(@"c:\logs\website.com.access.2018.04.15.log", item));
        }
    }
}
