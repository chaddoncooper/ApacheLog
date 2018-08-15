using Apache.Log.Data;
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
        private readonly ApacheLogContext _apacheLogContext;

        public Whitelist(ApacheLogContext apacheLogContext)
        {            
            _apacheLogContext = apacheLogContext;
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

            return _apacheLogContext.WhitelistedResources.Any(x => x.BasePath.Contains(requestResource)) ? true : false;
        }
    }
}
