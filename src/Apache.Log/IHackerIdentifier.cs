using Apache.Log.Models;

namespace Apache.Log
{
    public interface IHackerIdentifier
    {
        bool RequestedResourceIsKnownTarget(AccessRequest accessRequest);
        bool RequestedResourceIsWhitelisted(AccessRequest accessRequest);
    }
}
