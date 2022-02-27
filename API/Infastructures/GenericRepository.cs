using API.Data;
using API.Data.EntityBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace API.Infastructures
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, IEntityBase
    {
        protected readonly ApplicationDbContext Context;

        protected DbSet<TEntity> DbSet;

        public GenericRepository(ApplicationDbContext context)
        {
            Context = context;
            DbSet = Context.Set<TEntity>();
        }

        public async Task Add(TEntity entity)
        {
            await DbSet.AddAsync(entity);
        }

        public void Delete(params object[] keyValues)
        {
            var entityExisting = this.DbSet.Find(keyValues);
            if (entityExisting != null)
            {
                this.DbSet.Remove(entityExisting);
                return;
            }
            throw new ArgumentNullException($"{string.Join(";", keyValues)} was not found in the {typeof(TEntity)}");
        }

        public async Task<IList<TEntity>> Find(Expression<Func<TEntity, bool>> condition)
        {
            return await DbSet.Where(condition).ToListAsync();
        }

        public async Task<IList<TEntity>> GetAll(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            IQueryable<TEntity> query = DbSet;

            if (orderBy != null)
                query = orderBy(query);

            return await query.ToListAsync();
        }

        public async Task<TEntity> GetById(params object[] keyValues)
        {
            return await DbSet.FindAsync(keyValues);
        }

        public void Update(TEntity entity)
        {
            DbSet.Update(entity);
        }
    }
}
