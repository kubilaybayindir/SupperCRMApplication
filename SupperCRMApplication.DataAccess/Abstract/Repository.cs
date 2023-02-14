using Microsoft.EntityFrameworkCore;
using SupperCRMApplication.DataAccess.Context;
using SupperCRMApplication.Entities.Abstract;
using System.Linq.Expressions;

namespace SupperCRMApplication.DataAccess.Abstract
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : EntityBase
    {
        private readonly DatabaseContext _context;
        protected readonly DbSet<TEntity> _set;
        public Repository(DatabaseContext context)
        {
            _context = context;
            _set = _context.Set<TEntity>();
        }
        public virtual TEntity Add(TEntity model)
        {
            _set.Add(model);
            _context.SaveChanges();
            if (_context.SaveChanges() > 0)
                return model;

            return null;
        }

        public virtual TEntity Get(int id)
        {
            return _set.Find(id);
        }

        public virtual List<TEntity> GetAll()
        {
            return _set.ToList();
        }
        //predicate: where ifadeleri bir expressiondan oluşur.
        //Bir fonksiyon  alıp Enttiy alıp bool döner.  
        public virtual List<TEntity> GetAll(Expression<Func<TEntity,bool>> predicate)
        {
            return _set.Where(predicate).ToList();
        }
        public virtual void Remove(int id)
        {
            _set.Remove(Get(id));
            if (_context.SaveChanges() == 0)
                throw new Exception("Silme İşlemi Yapılamadı");
        }

        public virtual void Update(TEntity model)
        {
            if (model.Id == 0)
                throw new ArgumentNullException(nameof(model.Id));

            var entity = _context.Entry(model);
            entity.State = EntityState.Modified;

            if (_context.SaveChanges() == 0)
                throw new Exception("Güncelleme İşlemi Yapılamadı");
        }
    }
}