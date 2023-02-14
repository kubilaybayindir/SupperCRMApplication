using SupperCRMApplication.DataAccess.Abstract;
using SupperCRMApplication.DataAccess.Context;
using SupperCRMApplication.Entities;

namespace SupperCRMApplication.DataAccess
{

    public interface IUserRepository : IRepository<User>
    {

    }

    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly DatabaseContext _context;
        public UserRepository(DatabaseContext context) : base(context)
        {
            _context = context;
        }
    }

}