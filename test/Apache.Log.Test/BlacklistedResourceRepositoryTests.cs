using Apache.Log.Data.Entities;
using Apache.Log.Test.Factories;
using System.Linq;
using Xunit;

namespace Apache.Log.Test
{
    public class BlacklistedResourceRepositoryTests
    {
        private readonly IBlacklistedResourceRepositoryFactory _blacklistedResourceRepository;

        public BlacklistedResourceRepositoryTests()
        {
            _blacklistedResourceRepository = new BlacklistedResourceRepositoryFactory();
        }

        [Fact]
        public void BlacklistedResourceRepository_Add_ShouldNotSaveEntitiesWithExistingFullPaths()
        {
            using (var context = _blacklistedResourceRepository.New())
            {
                // Arrange
                for (var i = 0; i < 2; i++)
                {
                    var blacklistedResource = new BlacklistedResource()
                    {
                        FullPath = @"/admin/mysql2/index.php"
                    };
                    // Act
                    context.Add(blacklistedResource);
                    context.SaveChanges();
                }

                // Assert
                Assert.Single(context.GetAll());
            }
        }

        [Fact]
        public async void BlacklistedResourceRepository_AddAsync_ShouldNotSaveEntitiesWithExistingFullPathsAsync()
        {
            using (var context = _blacklistedResourceRepository.New())
            {
                // Arrange
                for (var i = 0; i < 2; i++)
                {
                    var blacklistedResource = new BlacklistedResource()
                    {
                        FullPath = @"/admin/mysql2/index.php"
                    };
                    // Act
                    await context.AddAsync(blacklistedResource);
                    await context.SaveChangesAsync();
                }

                // Assert
                Assert.Single(context.GetAll());
            }
        }

        [Fact]
        public void BlacklistedResourceRepository_Edit_ShouldNotSaveEntitiesWithExistingFullPaths()
        {
            using (var context = _blacklistedResourceRepository.New())
            {
                // Arrange
                for (var i = 0; i < 2; i++)
                {
                    var blacklistedResource = new BlacklistedResource()
                    {
                        FullPath = $@"/admin/mysql2/index{i}.php"
                    };
                    context.Add(blacklistedResource);
                    context.SaveChanges();
                }

                var secondEntity = context.FindBy(x => x.FullPath == @"/admin/mysql2/index1.php").Single();
                secondEntity.FullPath = @"/admin/mysql2/index0.php";
                // Act
                context.Edit(secondEntity);
                context.SaveChanges();

                var collection = context.GetAll().ToList();

                // Assert
                Assert.Collection(context.GetAll(),
                resource => Assert.Contains("/admin/mysql2/index0.php", resource.FullPath),
                resource => Assert.Contains("/admin/mysql2/index1.php", resource.FullPath)
                );
            }
        }
    }
}
