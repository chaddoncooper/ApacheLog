using Apache.Log.Data.Configurations;
using Apache.Log.Data.Entities;
using Apache.Log.Data.Seed;
using Microsoft.EntityFrameworkCore;

namespace Apache.Log.Data
{
    public class ApacheLogContext : DbContext
    {
        public ApacheLogContext(DbContextOptions<ApacheLogContext> options)
        : base(options)
        { }

        public virtual DbSet<WhitelistedResource> WhitelistedResources { get; set; }
        public virtual DbSet<BlacklistedResource> BlacklistedResources { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Configurations(modelBuilder);
            Seeds(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        private void Configurations(ModelBuilder modelBuilder)
        {
            new BlacklistedResourceConfiguration(modelBuilder.Entity<BlacklistedResource>());
            new WhitelistedResourceConfiguration(modelBuilder.Entity<WhitelistedResource>());
        }

        private void Seeds(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BlacklistedResource>().HasData(BlacklistedResourceSeed.Data());
        }
    }
}

