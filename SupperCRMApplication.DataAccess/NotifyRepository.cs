using SupperCRMApplication.DataAccess.Abstract;
using SupperCRMApplication.DataAccess.Context;
using SupperCRMApplication.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupperCRMApplication.DataAccess
{
    public interface INotifyRepository : IRepository<Notify>
    {
    }

    public class NotifyRepository:Repository<Notify>,INotifyRepository
    {
        private readonly DatabaseContext _context;

        public NotifyRepository(DatabaseContext context) : base(context)
        {
            _context = context;           
        }
    }
}
