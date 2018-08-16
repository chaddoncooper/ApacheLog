using Apache.Log.Data;
using Apache.Log.Models;
using System.Linq;

namespace Apache.Log.Resource
{
    public interface IBlacklist
    {
        bool RequestedResourceIsBlacklisted(AccessRequest accessRequest);
    }

    public class Blacklist : IBlacklist
    {
        private readonly ApacheLogContext _apacheLogContext;

        public Blacklist(ApacheLogContext apacheLogContext)
        {
            _apacheLogContext = apacheLogContext;
        }

        public bool RequestedResourceIsBlacklisted(AccessRequest accessRequest)
        {
            return _apacheLogContext.BlacklistedResources.Where(x => x.FullPath == accessRequest.Resource).Any();
        }
    }
}
