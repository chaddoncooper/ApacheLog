using Apache.Log.Data;
using Apache.Log.Data.Entities;
using Core.Repository;
using Apache.Log.Repository.Predicates;
using System;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Threading.Tasks;

namespace Apache.Log.Repository
{
    public interface IBlacklistedResourceRepository : IEntityBaseRepository<BlacklistedResource>, IDisposable { }

    public class BlacklistedResourceRepository : EntityBaseRepository<BlacklistedResource>, IBlacklistedResourceRepository, IDisposable
    {
        public BlacklistedResourceRepository(ApacheLogContext context) : base(context)
        {
        }

        public override void Add(BlacklistedResource entity)
        {
            _context.Set<BlacklistedResource>().AddIfNotExists(entity, x => x.FullPath == entity.FullPath);
        }

        public override async Task<EntityEntry<BlacklistedResource>> AddAsync(BlacklistedResource entity)
        {
            return await _context.Set<BlacklistedResource>().AddAsyncIfNotExists(entity, x => x.FullPath == entity.FullPath);
        }

        public override void Edit(BlacklistedResource entity)
        {
            var dbEntityEntry = _context.Entry(entity);
            _context.Set<BlacklistedResource>().EditIfNotExists(dbEntityEntry, x => x.FullPath == entity.FullPath);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
