using API.Data.EntityBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace API.Infastructures
{
    public interface IGenericRepository<TEntity> where TEntity : class, IEntityBase
    {
        Task<IList<TEntity>> GetAll(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null);

        Task<TEntity> GetById(params object[] keyValues);

        Task Add(TEntity entity);

        void Update(TEntity entity);

        void Delete(params object[] keyValues);

        Task<IList<TEntity>> Find(Expression<Func<TEntity, bool>> condition);
    }
}