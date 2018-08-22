using Apache.Log.Data.Entities;
using Apache.Log.Test.Factories;
using System.Linq;
using Xunit;

namespace Apache.Log.Test
{
    public class WhitelistedResourceRepositoryTests
    {
        private readonly IWhitelistedResourceRepositoryFactory _whitelistedResourceRepository;

        public WhitelistedResourceRepositoryTests()
        {
            _whitelistedResourceRepository = new WhitelistedResourceRepositoryFactory();
        }

        [Fact]
        public void WhitelistedResourceRepository_Add_ShouldNotSaveEntitiesWithExistingBasePaths()
        {
            using (var context = _whitelistedResourceRepository.New())
            {
                // Arrange
                for (var i = 0; i < 2; i++)
                {
                    var whitelistedResouce = new WhitelistedResource()
                    {
                        BasePath = @"index.php"
                    };
                    // Act
                    context.Add(whitelistedResouce);
                    context.SaveChanges();
                }

                // Assert
                Assert.Single(context.GetAll());
            }
        }

        [Fact]
        public async void WhitelistedResourceRepository_AddAsync_ShouldNotSaveEntitiesWithExistingBasePaths()
        {
            using (var context = _whitelistedResourceRepository.New())
            {
                // Arrange
                for (var i = 0; i < 2; i++)
                {
                    var whitelistedResouce = new WhitelistedResource()
                    {
                        BasePath = @"index.php"
                    };
                    // Act
                    await context.AddAsync(whitelistedResouce);
                    await context.SaveChangesAsync();
                }

                // Assert
                Assert.Single(context.GetAll());
            }
        }

        [Fact]
        public void WhitelistedResourceRepository_Edit_ShouldNotSaveEntitiesWithExistingBasePaths()
        {
            using (var context = _whitelistedResourceRepository.New())
            {
                // Arrange
                for (var i = 0; i < 2; i++)
                {
                    var whitelistedResource = new WhitelistedResource()
                    {
                         BasePath = $@"index{i}.php"
                    };
                    context.Add(whitelistedResource);
                    context.SaveChanges();
                }

                var secondEntity = context.FindBy(x => x.BasePath == @"index1.php").Single();
                secondEntity.BasePath = @"index0.php";
                // Act
                context.Edit(secondEntity);
                context.SaveChanges();

                var collection = context.GetAll().ToList();

                // Assert
                Assert.Collection(context.GetAll(),
                resource => Assert.Contains("index0.php", resource.BasePath),
                resource => Assert.Contains("index1.php", resource.BasePath)
                );
            }
        }
    }
}
