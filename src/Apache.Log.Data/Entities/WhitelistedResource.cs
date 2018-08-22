using Core.Repository;

namespace Apache.Log.Data.Entities
{
    public class WhitelistedResource : IEntityBase
    {
        public int Id { get; set; }
        public string BasePath { get; set; }
    }
}
