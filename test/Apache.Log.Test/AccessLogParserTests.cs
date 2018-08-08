using Apache.Log.AccessLog;
using Apache.Log.Configuration;
using Apache.Log.Models;
using System;
using System.Collections.Generic;
using System.IO.Abstractions.TestingHelpers;
using Xunit;

namespace Apache.Log.Test
{
    public class AccessLogParserTests
    {

        [Fact]
        public void Should_ReturnAResourceURL_FromLineOfLog()
        {
            // Arrange
            var accessLogParser = new Parser(new MockFileSystem(), AccessLogConfig.GetDefault());
            var testLine = @"140.143.233.233 - - [14/May/2018:03:04:31 +0100] ""GET /admin/mysql2/index.php HTTP/1.1"" 301 251";
            var accessRequest = new AccessRequest();

            // Act
            var result = accessLogParser.Parse(testLine, out accessRequest);

            // Assert
            Assert.True(result);
            Assert.Equal("/admin/mysql2/index.php", accessRequest.Resource);
        }

        [Fact]
        public void Should_ReturnADateTime_FromLineOfLog()
        {
            // Arrange
            var accessLogParser = new Parser(new MockFileSystem(), AccessLogConfig.GetDefault());
            var testLine = @"140.143.233.233 - - [14/May/2018:03:04:31 +0100] ""GET /admin/mysql2/index.php HTTP/1.1"" 301 251";
            var accessRequest = new AccessRequest();

            // Act
            var result = accessLogParser.Parse(testLine, out accessRequest);

            // Assert
            Assert.True(result);
            Assert.Equal(new DateTimeOffset(2018, 05, 14, 03, 04, 31, new TimeSpan(1, 0, 0)), accessRequest.DateTime);
        }

        [Fact]
        public void Should_ReturnAnIPAddress_FromLineOfLog()
        {
            // Arrange
            var accessLogParser = new Parser(new MockFileSystem(), AccessLogConfig.GetDefault());
            var testLine = @"140.143.233.233 - - [14/May/2018:03:04:31 +0100] ""GET /admin/mysql2/index.php HTTP/1.1"" 301 251";
            var accessRequest = new AccessRequest();

            // Act
            var result = accessLogParser.Parse(testLine, out accessRequest);

            // Assert
            Assert.True(result);
            Assert.Equal("140.143.233.233", accessRequest.IPAddress);
        }

        [Fact]
        public void Should_ReturnProtocolMethod_FromLineOfLog()
        {
            // Arrange
            var accessLogParser = new Parser(new MockFileSystem(), AccessLogConfig.GetDefault());
            var testLine = @"140.143.233.233 - - [14/May/2018:03:04:31 +0100] ""GET /admin/mysql2/index.php HTTP/1.1"" 301 251";
            var accessRequest = new AccessRequest();

            // Act
            var result = accessLogParser.Parse(testLine, out accessRequest);

            // Assert
            Assert.True(result);
            Assert.Equal("HTTP/1.1", accessRequest.Protocol);
        }

        [Fact]
        public void Should_ReturnGETMethod_FromLineOfLog()
        {
            // Arrange
            var accessLogParser = new Parser(new MockFileSystem(), AccessLogConfig.GetDefault());
            var testLine = @"140.143.233.233 - - [14/May/2018:03:04:31 +0100] ""GET /admin/mysql2/index.php HTTP/1.1"" 301 251";
            var accessRequest = new AccessRequest();

            // Act
            var result = accessLogParser.Parse(testLine, out accessRequest);

            // Assert
            Assert.True(result);
            Assert.Equal("GET", accessRequest.Method);
        }

        [Fact]
        public void Should_ReturnStatusCodeMethod_FromLineOfLog()
        {
            // Arrange
            var accessLogParser = new Parser(new MockFileSystem(), AccessLogConfig.GetDefault());
            var testLine = @"140.143.233.233 - - [14/May/2018:03:04:31 +0100] ""GET /admin/mysql2/index.php HTTP/1.1"" 301 251";
            var accessRequest = new AccessRequest();

            // Act
            var result = accessLogParser.Parse(testLine, out accessRequest);

            // Assert
            Assert.True(result);
            Assert.Equal(301, accessRequest.StatusCode);
        }

        [Fact]
        public void Should_ReturnSizeMethod_FromLineOfLog()
        {
            // Arrange
            var accessLogParser = new Parser(new MockFileSystem(), AccessLogConfig.GetDefault());
            var testLine = @"140.143.233.233 - - [14/May/2018:03:04:31 +0100] ""GET /admin/mysql2/index.php HTTP/1.1"" 301 251";
            var accessRequest = new AccessRequest();

            // Act
            var result = accessLogParser.Parse(testLine, out accessRequest);

            // Assert
            Assert.True(result);
            Assert.Equal(251, accessRequest.Size);
        }

        [Fact]
        public void Should_ReturnAccessRequests_FromALogFile()
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
                }
            });

            var accessLogParser = new Parser(fileSystem, AccessLogConfig.GetDefault());

            // Act
            var accessRequests = accessLogParser.Parse(@"c:\logs\website.com.access.2018.04.13.log");

            // Assert
            Assert.Collection(accessRequests, item => Assert.Contains("/pmamy/index.php", item.Resource),
                              item => Assert.Contains("/pmamy2/index.php", item.Resource),
                              item => Assert.Contains("/mysql/index.php", item.Resource),
                              item => Assert.Contains("/admin/index.php", item.Resource));
        }

        
    }
}
