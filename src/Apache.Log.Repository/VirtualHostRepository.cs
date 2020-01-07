using Apache.Log.Data;
using Apache.Log.Data.Entities;
using Core.Repository;

namespace Apache.Log.Repository
{
    public interface IVirtualHostRepository : IEntityBaseRepository<VirtualHost> { }

    public class VirtualHostRepository : EntityBaseRepository<VirtualHost>, IVirtualHostRepository
    {
        public VirtualHostRepository(ApacheLogContext context) : base(context)
        {
        }
    }
}
