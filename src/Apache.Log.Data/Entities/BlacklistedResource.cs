using Core.Repository;

namespace Apache.Log.Data.Entities
{
    public class BlacklistedResource : IEntityBase
    {
        public int Id { get; set; }
        public string FullPath { get; set; }
    }
}
