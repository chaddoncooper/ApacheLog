using Apache.Log.API.Controllers;
using Apache.Log.Data.Entities;
using Apache.Log.Test.Factories;
using System.Linq;
using Xunit;

namespace Apache.Log.Test
{
    public class BlacklistedResourcesControllerTests
    {
        private readonly IBlacklistedResourceRepositoryFactory _blacklistedRepositoryFactory;

        public BlacklistedResourcesControllerTests()
        {
            _blacklistedRepositoryFactory = new BlacklistedResourceRepositoryFactory();
        }

        [Fact]
        public void GetBlacklistedResources_ReturnsAnIEnumerable_OfWhitelistedResources()
        {
            using (var blacklist = _blacklistedRepositoryFactory.New())
            {
                blacklist.Add(new BlacklistedResource() { FullPath = @"/admin/mysql2/index.php" });
                blacklist.Add(new BlacklistedResource() { FullPath = @"/admin/mysql/index.php" });
                blacklist.SaveChanges();

                var controller = new BlacklistedResourcesController(blacklist);

                var result = controller.GetBlacklistedResources();

                
                Assert.Equal(2, result.Count());
            }
        }
    }
}
