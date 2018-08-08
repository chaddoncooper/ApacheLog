using Apache.Log.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Apache.Log.Data
{
    public class ApacheLogContext : DbContext
    {
        public ApacheLogContext(DbContextOptions<ApacheLogContext> options)
        : base(options)
        { }

        public DbSet<WhitelistedResource> WhitelistedResources{ get; set; }
        public DbSet<BlacklistedResource> BlacklistedResources { get; set; }
    }
}
