using Microsoft.EntityFrameworkCore;
using SupperCRMApplication.DataAccess.Abstract;
using SupperCRMApplication.DataAccess.Context;
using SupperCRMApplication.Entities;

namespace SupperCRMApplication.DataAccess
{
    public interface IClientRepository : IRepository<Client>
    {
        
    }

    public class ClientRepository : Repository<Client>, IClientRepository
    {
        private readonly DatabaseContext _context;
        public ClientRepository(DatabaseContext context) : base(context)
        {
            _context = context;
        }
    }
}