using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SupperCRMApplication.DataAccess;
using SupperCRMApplication.DataAccess.Abstract;
using SupperCRMApplication.Entities;
using SupperCRMApplication.Entities.Abstract;
using SupperCRMApplication.Models;

namespace SupperCRMApplication.Services
{
    public interface IClientService 
    {
        void Create(string name, string email);
        void Create(CreateCustomerModel  model);
        List<Client> List();
        Client GetById(int id);
        void Update(int id, EditCustomerModel model);
        void Delete(int id);
        List<Client>? ListBySearch(string search);
        
    }

    public class ClientService:ServiceBase<Client,IClientRepository>,IClientService
    {

        private readonly IClientRepository _repository;

        public ClientService(IClientRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public List<Client> List()
        {
            return _repository.GetAll();
        }

        public void Create(string name, string email)
        {
            Client client = new Client
            {
                Name = name,
                Email = email,
                CreatedAt = System.DateTime.Now,

            };
            _repository.Add(client);
        }

        public void Create(CreateCustomerModel model)
        {
            Client client = new Client
            {
                Name = model.Name,
                Email = model.Email,
                Description = model.Description,
                Phone = model.Phone,
                Locked = model.Locked,
                IsCorporate = model.IsCorporate,
                CreatedAt = System.DateTime.Now,

            };
            _repository.Add(client);
        }
        public void Update(int id, EditCustomerModel model)
        {

            Client client = _repository.Get(id);

            client.Name = model.Name;
            client.Email = model.Email;
            client.Description = model.Description;
            client.Phone = model.Phone;
            client.IsCorporate = model.IsCorporate;
            client.Locked = model.Locked;

            _repository.Update(client);
        }
        public Client GetById(int id)
        {
            return _repository.Get(id);
        }
        public void Delete(int id)
        {
            _repository.Remove(id);
        }
        public List<Client>? ListBySearch(string search)
        {
            return _repository.GetAll(x =>
                    x.Name.Contains(search) ||
                    x.Email.Contains(search) ||
                    x.Phone.Contains(search) ||
                    x.Description.Contains(search));
        }
    }
}