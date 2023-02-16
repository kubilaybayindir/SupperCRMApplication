using Microsoft.EntityFrameworkCore;
using SupperCRMApplication.DataAccess.Abstract;
using SupperCRMApplication.DataAccess.Context;
using SupperCRMApplication.Entities;
using System.Linq.Expressions;

namespace SupperCRMApplication.DataAccess
{
    public interface ILogRepository : IRepository<Log>
    {
    }
    public class LogRepository:Repository<Log>, ILogRepository
    {
        private readonly DatabaseContext _context;

        public LogRepository(DatabaseContext context) : base(context)
        {
            _context = context;
        }

        public override List<Log> GetAll()
        {
            return _set.Include(x => x.User).ToList();
        }

        public override List<Log> GetAll(Expression<Func<Log,bool>> predicate)
        {
            return _set.Include(x => x.User).Where(predicate).ToList();
        }

        public override Log Get(int id)
        {
            return _set.Include(x => x.User).SingleOrDefault(x => x.Id == id);
        }
    }
}
