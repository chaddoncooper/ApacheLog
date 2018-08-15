using Apache.Log.Data.Entities;
using Apache.Log.Models;
using Apache.Log.Resource;
using Apache.Log.Test.TestFactories;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Apache.Log.Test
{
    public class WhitelistTests
    {
        private IApacheLogContextFactory _apacheLogContextFactory;

        public WhitelistTests()
        {
            _apacheLogContextFactory = new ApacheLogContextFactory();
        }

        [Fact]
        public void RequestedResourceIsWhitelisted_ReturnsTrue_IfRequestResourceIsWhitelisted()
        {
            using (var context = _apacheLogContextFactory.NewTestContext())
            {
                // Arrange
                var whitelistedResources = new WhitelistedResource[3]
                {
                new WhitelistedResource() { BasePath = "media" },
                new WhitelistedResource() { BasePath = "index.php" },
                new WhitelistedResource() { BasePath = "script.cgi" }
                };

                context.WhitelistedResources.AddRange(whitelistedResources);
                context.SaveChanges();

                var whitelist = new Whitelist(context);
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
        }

        [Fact]
        public void RequestedResourceIsWhitelisted_ReturnsFalse_IfRequestResourceIsNotWhitelisted()
        {
            using (var context = _apacheLogContextFactory.NewTestContext())
            {
                // Arrange
                context.WhitelistedResources.Add(new WhitelistedResource() { BasePath = "media" });
                context.SaveChanges();

                // Arrange
                var whitelistedResources = new List<string>() { @"media" }.AsQueryable(); ;
                var whitelist = new Whitelist(context);
                var accessRequest = new AccessRequest() { Resource = @"/admin/mysql2/index.php" };

                // Act
                var result = whitelist.RequestedResourceIsWhitelisted(accessRequest);

                // Assert
                Assert.False(result);
            }
        }
    }
}
