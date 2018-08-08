using Apache.Log.Models;
using System;
using System.Linq;

namespace Apache.Log.Resource
{
    public interface IWhitelist
    {
        bool RequestedResourceIsWhitelisted(AccessRequest accessRequest);
    }

    public class Whitelist : IWhitelist
    {
        private readonly IQueryable<string> _whitelistedResources;

        public Whitelist(IQueryable<string> whitelistedResources)
        {
            _whitelistedResources = whitelistedResources;
        }

        public bool RequestedResourceIsWhitelisted(AccessRequest accessRequest)
        {
            var requestResource = accessRequest.Resource;
            try
            {
                requestResource = accessRequest.Resource.Split('/', 3)[1];
            }
            catch (IndexOutOfRangeException)
            {
                requestResource = accessRequest.Resource;
            }

            return _whitelistedResources.Any(x => x.StartsWith(requestResource)) ? true : false;
        }
    }
}
