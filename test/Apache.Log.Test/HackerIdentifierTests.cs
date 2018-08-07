using Apache.Log.Models;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Apache.Log.Test
{
    public class HackerIdentifierTests
    {
        [Fact]
        public void RequestedResourceIsWhitelisted_ReturnsFalse_IfRequestResourceIsNotWhitelisted()
        {
            // Arrange
            var whitelist = new List<string>() { @"media" }.AsQueryable();
            var hackerIdentifier = new HackerIdentifier(null, whitelist);
            var accessRequest = new AccessRequest() { Resource = @"/admin/mysql2/index.php" };

            // Act
            var result = hackerIdentifier.RequestedResourceIsWhitelisted(accessRequest);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void RequestedResourceIsWhitelisted_ReturnsTrue_IfRequestResourceIsWhitelisted()
        {
            // Arrange
            var whitelist = new List<string>() { @"media" }.AsQueryable(); ;
            var hackerIdentifier = new HackerIdentifier(null, whitelist);
            var accessRequest = new AccessRequest() { Resource = @"/media/rest/getPlaylists.view" };

            // Act
            var result = hackerIdentifier.RequestedResourceIsWhitelisted(accessRequest);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void RequestedResourceIsKnownTarget_ReturnsTrue_IfRequesedResourceIsNotKnownTarget()
        {
            // Arrange
            var knownTargets = new List<string>() { @"/admin/mysql2/index.php" }.AsQueryable();
            var hackerIdentifier = new HackerIdentifier(knownTargets, null);
            var accessRequest = new AccessRequest() { Resource = @"/admin/" };

            // Act
            var result = hackerIdentifier.RequestedResourceIsKnownTarget(accessRequest);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void RequestedResourceIsKnownTarget_ReturnsTrue_IfRequesedResourceIsKnownTarget()
        {
            // Arrange
            var knownTargets = new List<string>() { @"/admin/mysql2/index.php" }.AsQueryable();
            var hackerIdentifier = new HackerIdentifier(knownTargets, null);
            var accessRequest = new AccessRequest() { Resource = @"/admin/mysql2/index.php" };

            // Act
            var result = hackerIdentifier.RequestedResourceIsKnownTarget(accessRequest);

            // Assert
            Assert.True(result);
        }

        // Write tests for RequestedResourceIsWhitelisted
        // check /index.php
        // check script.cgi
    }
}
