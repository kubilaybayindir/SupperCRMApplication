using Microsoft.EntityFrameworkCore;
using SupperCRMApplication.DataAccess.Abstract;
using SupperCRMApplication.DataAccess.Context;
using SupperCRMApplication.Entities;
using System.Linq.Expressions;

namespace SupperCRMApplication.DataAccess
{
    public interface ILeadRepository : IRepository<Lead>
    {
    }
    public class LeadRepository : Repository<Lead>, ILeadRepository
    {
        private readonly DatabaseContext _context;

        public LeadRepository(DatabaseContext context) : base(context)
        {
            _context = context;
        }

        public override List<Lead> GetAll()
        {
            return _set.Include(x => x.User).Include(x => x.Client).ToList();
        }

        public override List<Lead> GetAll(Expression<Func<Lead, bool>> predicate)
        {
            return _set.Include(x => x.User).Include(x => x.Client).Where(predicate).ToList();
        }

        public override Lead Get(int id)
        {
            return _set.Include(x => x.User).Include(x => x.Client).SingleOrDefault(x => x.Id == id);
        }
    }
}