using Apache.Log.Models;
using Apache.Log.Resource;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Apache.Log.Test
{
    public class WhitelistTests
    {
        [Fact]
        public void RequestedResourceIsWhitelisted_ReturnsTrue_IfRequestResourceIsWhitelisted()
        {
            // Arrange
            var whitelistedResources = new List<string>()
            {
                "media",
                "index.php",
                "script.cgi"
            }
            .AsQueryable();

            var whitelist = new Whitelist(whitelistedResources);
            var accessRequests = new List<AccessRequest>()
            {
                new AccessRequest() { Resource = @"/media/rest/getPlaylists.view" },
                new AccessRequest() { Resource = @"/index.php" },
                new AccessRequest() { Resource = @"script.cgi" }
            };

            foreach (var accessRequest in accessRequests)
            {
                // Act
                var result = whitelist.RequestedResourceIsWhitelisted(accessRequest);

                // Assert
                Assert.True(result);
            }
        }

        [Fact]
        public void RequestedResourceIsWhitelisted_ReturnsFalse_IfRequestResourceIsNotWhitelisted()
        {
            // Arrange
            var whitelistedResources = new List<string>() { @"media" }.AsQueryable(); ;
            var whitelist = new Whitelist(whitelistedResources);
            var accessRequest = new AccessRequest() { Resource = @"/admin/mysql2/index.php" };

            // Act
            var result = whitelist.RequestedResourceIsWhitelisted(accessRequest);

            // Assert
            Assert.False(result);
        }
    }
}
