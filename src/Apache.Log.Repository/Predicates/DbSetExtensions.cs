using Core.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Apache.Log.Repository.Predicates
{
    public static class DbSetExtensions
    {
        public static EntityEntry<T> AddIfNotExists<T>(this DbSet<T> dbSet, T entity, Expression<Func<T, bool>> predicate = null) where T : class, new()
        {
            var exists = predicate != null ? dbSet.Any(predicate) : dbSet.Any();
            return !exists ? dbSet.Add(entity) : null;
        }

        public async static Task<EntityEntry<T>> AddAsyncIfNotExists<T>(this DbSet<T> dbSet, T entity, Expression<Func<T, bool>> predicate = null) where T : class, new()
        {
            var exists = predicate != null ? await dbSet.AnyAsync(predicate) : dbSet.Any();
            return !exists ? await dbSet.AddAsync(entity) : null;
        }

        public static void EditIfNotExists<T>(this DbSet<T> dbSet, EntityEntry<T> entity, Expression<Func<T, bool>> predicate = null) where T : class, new()
        {
            var exists = predicate != null ? dbSet.Any(predicate) : dbSet.Any();
            if (exists)
            {
                entity.State = EntityState.Unchanged;
            }   
        }
    }
}
