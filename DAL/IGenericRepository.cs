using System;
using System.Linq;
using System.Linq.Expressions;
namespace DAL
{
    interface IGenericRepository<TEntity>
     where TEntity : class
    {
        void Delete(object id);
        void Delete(TEntity entityToDelete);
        System.Collections.Generic.IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");
        TEntity GetById(object id);
        void Insert(TEntity entity);
        void Update(TEntity entityToUpdate);
        void AttachStubs(object[] stub);
    }
}
