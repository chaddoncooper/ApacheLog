using Apache.Log.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders; 

namespace Apache.Log.Data.Configurations
{
    public class WhitelistedResourceConfiguration
    {
        public WhitelistedResourceConfiguration(EntityTypeBuilder<WhitelistedResource> entityTypeBuilder)
        {
            entityTypeBuilder.HasIndex(u => u.BasePath).IsUnique();
        }
    }
}
