﻿using Apache.Log.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Apache.Log.Data.Configurations
{
    class BlacklistedResourceConfiguration
    {
        private EntityTypeBuilder<BlacklistedResource> entityTypeBuilder;

        public BlacklistedResourceConfiguration(EntityTypeBuilder<BlacklistedResource> entityTypeBuilder)
        {
            this.entityTypeBuilder = entityTypeBuilder;
        }
    }
}
