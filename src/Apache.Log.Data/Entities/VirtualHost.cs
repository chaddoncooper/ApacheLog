using Core.Repository;

namespace Apache.Log.Data.Entities
{
    public class VirtualHost : IEntityBase
    {
        public int Id { get; set; }
        public string HostName { get; set; }
        public string LogFileBaseDirectory { get; set; }
    }
}
