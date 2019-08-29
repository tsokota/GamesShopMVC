using System;
namespace DAL
{
    public interface INorthWindGenericRepository<TEntity> where TEntity : class
    {
        void Delete(object id);
        void Delete(TEntity entityToDelete);
     
        System.Collections.Generic.IEnumerable<TEntity> Get(System.Linq.Expressions.Expression<Func<TEntity, bool>> filter = null, Func<System.Linq.IQueryable<TEntity>, System.Linq.IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");
        TEntity GetById(object id);
        void Update(TEntity entityToUpdate);
    }
}
