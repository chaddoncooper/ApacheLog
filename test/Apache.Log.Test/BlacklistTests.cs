using Apache.Log.Data.Entities;
using Apache.Log.Models;
using Apache.Log.Resource;
using Apache.Log.Test.TestFactories;
using Xunit;

namespace Apache.Log.Test
{
    public class BlacklistTests
    {
        private readonly IApacheLogContextFactory _apacheLogContextFactory;

        public BlacklistTests()
        {
            _apacheLogContextFactory = new ApacheLogContextFactory();
        }

        [Fact]
        public void RequestedResourceIsBlacklisted_ReturnsFalse_IfRequesedResourceIsNotBlacklisted()
        {
            using (var context = _apacheLogContextFactory.NewTestContext())
            {
                // Arrange
                context.Add(new BlacklistedResource() { FullPath = @"/admin/mysql2/index.php" });
                context.SaveChanges();
                var blacklist = new Blacklist(context);
                var accessRequest = new AccessRequest() { Resource = @"/admin/" };

                // Act
                var result = blacklist.RequestedResourceIsBlacklisted(accessRequest);

                // Assert
                Assert.False(result);
            }
        }

        [Fact]
        public void RequestedResourceIsBlacklisted_ReturnsTrue_IfRequesedResourceIsBlacklisted()
        {
            using (var context = _apacheLogContextFactory.NewTestContext())
            {
                // Arrange
                context.Add(new BlacklistedResource() { FullPath = @"/admin/mysql2/index.php" });
                context.SaveChanges();
                var blacklist = new Blacklist(context);
                var accessRequest = new AccessRequest() { Resource = @"/admin/mysql2/index.php" };

                // Act
                var result = blacklist.RequestedResourceIsBlacklisted(accessRequest);

                // Assert
                Assert.True(result);
            }
        }
    }
}
