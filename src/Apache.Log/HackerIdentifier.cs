using System;
using System.Linq;
using Apache.Log.Models;

namespace Apache.Log
{
    public class HackerIdentifier : IHackerIdentifier
    {
        private readonly IQueryable<string> _knownTargetedResources;
        private readonly IQueryable<string> _whitelist;

        public HackerIdentifier(IQueryable<string> knownTargetedResources, IQueryable<string> whitelist)
        {
            _knownTargetedResources = knownTargetedResources;
            _whitelist = whitelist;
        }

        public bool RequestedResourceIsWhitelisted(AccessRequest accessRequest)
        {
            var requestResource = accessRequest.Resource.Split('/', 3)[1];
            return _whitelist.Any(x => x.StartsWith(requestResource)) ? true : false;
        }

        public bool RequestedResourceIsKnownTarget(AccessRequest accessRequest)
        {
            return _knownTargetedResources.Contains(accessRequest.Resource);
        }
    }
}
