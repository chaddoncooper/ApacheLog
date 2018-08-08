using Apache.Log.Models;
using Apache.Log.Resource;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Apache.Log.Test
{
    public class BlacklistTests
    {
        [Fact]
        public void RequestedResourceIsBlacklisted_ReturnsTrue_IfRequesedResourceIsNotBlacklisted()
        {
            // Arrange
            var blacklistedResources = new List<string>() { @"/admin/mysql2/index.php" }.AsQueryable();
            var blacklist = new Blacklist(blacklistedResources);
            var accessRequest = new AccessRequest() { Resource = @"/admin/" };

            // Act
            var result = blacklist.RequestedResourceIsBlacklisted(accessRequest);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void RequestedResourceIsBlacklisted_ReturnsTrue_IfRequesedResourceIsBlacklisted()
        {
            // Arrange
            var blacklistedResources = new List<string>() { @"/admin/mysql2/index.php" }.AsQueryable();
            var blacklist = new Blacklist(blacklistedResources);
            var accessRequest = new AccessRequest() { Resource = @"/admin/mysql2/index.php" };

            // Act
            var result = blacklist.RequestedResourceIsBlacklisted(accessRequest);

            // Assert
            Assert.True(result);
        }
    }
}
