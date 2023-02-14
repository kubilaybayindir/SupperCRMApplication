using SupperCRMApplication.Entities.Abstract;
using System.Linq.Expressions;

namespace SupperCRMApplication.DataAccess.Abstract
{
    public interface IRepository<TEntity>
    where TEntity : EntityBase
    {
        TEntity Add(TEntity model);
        TEntity Get(int id);
        List<TEntity> GetAll();
        List<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate);
        void Remove(int id);
        void Update(TEntity model);
    }
}