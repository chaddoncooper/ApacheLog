using Apache.Log.Data.Entities;
using Core.Repository;
using Microsoft.EntityFrameworkCore;

namespace Apache.Log.Repository
{
    public interface IVirtualHostRepository : IEntityBaseRepository<VirtualHost> { }

    public class VirtualHostRepository : EntityBaseRepository<VirtualHost>, IVirtualHostRepository
    {
        public VirtualHostRepository(DbContext context) : base(context)
        {
        }
    }
}
