using Apache.Log.Models;
using System;
using System.Linq;

namespace Apache.Log.Resource
{
    public interface IBlacklist
    {
        bool RequestedResourceIsBlacklisted(AccessRequest accessRequest);
    }

    public class Blacklist
    {
        private readonly IQueryable<string> _blacklistedResources;

        public Blacklist(IQueryable<string> blacklistedResources)
        {
            _blacklistedResources = blacklistedResources;
        }

        public bool RequestedResourceIsBlacklisted(AccessRequest accessRequest)
        {
            return _blacklistedResources.Contains(accessRequest.Resource);
        }
    }
}
