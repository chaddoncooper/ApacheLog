using Apache.Log.Data;
using Apache.Log.Data.Entities;
using Apache.Log.Repository.Predicates;
using Core.Repository;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Threading.Tasks;

namespace Apache.Log.Repository
{
    public interface IWhitelistedResourceRepository : IEntityBaseRepository<WhitelistedResource>, IDisposable { }

    public class WhitelistedResourceRepository : EntityBaseRepository<WhitelistedResource>, IWhitelistedResourceRepository, IDisposable
    {
        public WhitelistedResourceRepository(ApacheLogContext context) : base(context)
        {

        }

        public override void Add(WhitelistedResource entity)
        {
            _context.Set<WhitelistedResource>().AddIfNotExists(entity, x => x.BasePath == entity.BasePath);
        }

        public override async Task<EntityEntry<WhitelistedResource>> AddAsync(WhitelistedResource entity)
        {
            return await _context.Set<WhitelistedResource>().AddAsyncIfNotExists(entity, x => x.BasePath == entity.BasePath);
        }

        public override void Edit(WhitelistedResource entity)
        {
            var dbEntityEntry = _context.Entry(entity);
            _context.Set<WhitelistedResource>().EditIfNotExists(dbEntityEntry, x => x.BasePath == entity.BasePath);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
