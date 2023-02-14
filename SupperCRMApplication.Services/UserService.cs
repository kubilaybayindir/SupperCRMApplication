﻿using NETCore.Encrypt.Extensions;
using SupperCRMApplication.Common;
using SupperCRMApplication.DataAccess;
using SupperCRMApplication.Entities;
using SupperCRMApplication.Models;

namespace SupperCRMApplication.Services
{
    public interface IUserService : IServiceBase<User>
    {
        User Authenticate(AuthenticateModel model);
        void ChangePassword(int id, ChangePasswordModel model);
        void ChangeUsername(int id, ChangeUsernameModel model);
        void Create(CreateUserModel model);
        List<User> ListBySearch(string search);
        void Update(int id, EditUserModel model);
    }
    public class UserService : ServiceBase<User, IUserRepository>, IUserService
    {
        private readonly IUserRepository _repository;
        public UserService(IUserRepository repository) : base(repository)
        {
            _repository = repository;
        }
        public User Authenticate(AuthenticateModel model)
        {
            model.Username = model.Username.Trim();
            model.Password = (Constants.PasswordSalt + model.Password).MD5();
            return _repository.GetAll(x => x.Username.ToLower() == model.Username.ToLower() && x.Password == model.Password).FirstOrDefault();
        }
        public void Create(CreateUserModel model)
        {
            string username = model.Username.Trim();
            //varolan bir kullanıcı var mı yok mu kontrol sağlanıyor 
            if(_repository.GetAll(x => x.Username.ToLower() == username).Count() == 0)
            {
                User user = new User
                {
                    Name = model.Name,
                    Email = model.Email,
                    Username = model.Username,
                    Password = (Constants.PasswordSalt + model.Password).MD5(),
                    Role = model.Role,
                    Locked = model.Locked,
                    CreatedAt = System.DateTime.Now,
                };
                _repository.Add(user);
            }
            else
            {
                throw new System.Exception("Kullanıcı adı zeten kullanılıyor.");
            }

        }
        public void Update(int id, EditUserModel model)
        {
            User user = _repository.Get(id);

            user.Name = model.Name;
            user.Email = model.Email;
            user.Role = model.Role;
            user.Locked = model.Locked;

            _repository.Update(user);
        }
        public void ChangePassword(int id, ChangePasswordModel model)
        {
            User user=_repository.Get(id);
            user.Password = (Constants.PasswordSalt + model.Password).MD5();

            _repository.Update(user);
        }
        public void ChangeUsername(int id, ChangeUsernameModel model)
        {
            string username = model.Username.Trim();
            //bu kullanıcı adıyla Id si benimle eşit olmayan başkası var mı kontrol ediliyor.
            if (_repository.GetAll(x => x.Username.ToLower() == username && x.Id !=id).Count() == 0)
            {
                User user=_repository.Get(id);
                user.Username = model.Username;

                _repository.Update(user);

            }
            else
            {
                throw new System.Exception("Kullanıcı adı zeten kullanılıyor.");
            }
        }
        public List<User> ListBySearch(string search)
        {
            return _repository.GetAll(x => 
            x.Name.Contains(search) || 
            x.Email.Contains(search) || 
            x.Role.Contains(search) || 
            x.Username.Contains(search));
        }
    }
    
}