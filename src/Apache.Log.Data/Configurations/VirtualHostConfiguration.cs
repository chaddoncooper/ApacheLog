using Apache.Log.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Apache.Log.Data.Configurations
{
    class VirtualHostConfiguration
    {
        private EntityTypeBuilder<VirtualHost> entityTypeBuilder;

        public VirtualHostConfiguration(EntityTypeBuilder<VirtualHost> entityTypeBuilder)
        {
            this.entityTypeBuilder = entityTypeBuilder;
        }
    }
}
