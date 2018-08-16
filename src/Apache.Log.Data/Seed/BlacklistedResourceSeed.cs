using Apache.Log.Data.Entities;

namespace Apache.Log.Data.Seed
{
    public static class BlacklistedResourceSeed
    {
        public static BlacklistedResource[] Data()
        {
            return new BlacklistedResource[]
            {
                new BlacklistedResource { Id = 1, FullPath = @"/phpmyadmin/main.php" },
                new BlacklistedResource { Id = 2, FullPath = @"/db/main.php" },
                new BlacklistedResource { Id = 3, FullPath = @"/web/main.php" },
                new BlacklistedResource { Id = 4, FullPath = @"/PMA/main.php" },
                new BlacklistedResource { Id = 5, FullPath = @"/dbadmin/main.php" },
                new BlacklistedResource { Id = 6, FullPath = @"/mysql/main.php" },
                new BlacklistedResource { Id = 7, FullPath = @"/phpmyadmin2/main.php" },
                new BlacklistedResource { Id = 8, FullPath = @"/phpmyadmin/read_dump.phpmain.php" },
                new BlacklistedResource { Id = 9, FullPath = @"/PMA/read_dump.phpmain.php" },
                new BlacklistedResource { Id = 10, FullPath = @"/mysql/read_dump.phpmain.php" },
                new BlacklistedResource { Id = 11, FullPath = @"/xampp/phpmyadmin/read_dump.phpmain.php" },
                new BlacklistedResource { Id = 12, FullPath = @"/typo3/phpmyadmin/read_dump.phpmain.php" },
                new BlacklistedResource { Id = 13, FullPath = @"/mysqladmin/read_dump.phpmain.php" },
                new BlacklistedResource { Id = 14, FullPath = @"/admin/read_dump.phpmain.php" },
                new BlacklistedResource { Id = 15, FullPath = @"/db/read_dump.phpmain.php" }
            };
        }
    }
}
