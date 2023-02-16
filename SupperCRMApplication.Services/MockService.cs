using SupperCRMApplication.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupperCRMApplication.Services
{
    public interface IMockService
    {
        void RunFakeGenerator();
    }
    public class MockService:IMockService
    {
        private readonly IMockRepository _repository;

        public MockService(IMockRepository repository)
        {
            _repository = repository;
        }
        public void RunFakeGenerator()
        {
            _repository.GenerateFakeData();
        }
    }


}
