using BLL.IServices;
using DataAccessLogic.ADO;
using DataAccessLogic.Interfaces;
using DTO.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ServiceAdmin : IServiceAdmin
    {
        readonly private IRepository<User> _adminRep;
        public ServiceAdmin()
        {
            _adminRep  = new Users(); 
        }

        public void Add(User tempObj, User currentAdmin)
        {

            if (tempObj.RoleId != 1 && currentAdmin.RoleId == 2)
            {
                _adminRep.Add(tempObj);
            }
            else
            {
                throw new Exception("Admin cannot add user with role 'Superadmin'");
            }

        }

        public void Delete(int id, User currentAdmin)
        {
            var userToDelete = _adminRep.GetById(id);

            if (currentAdmin.RoleId == 2)
            {
                if (userToDelete.RoleId != 1)
                {
                    _adminRep.DeleteById(id);
                }
                else
                {
                    throw new Exception("Admin cannot delete user with role 'Superadmin'");
                }
            }
        }

        public User GetByEmail(string email, User currentAdmin)
        {

            var user = _adminRep.GetAll().FirstOrDefault(u => u.Email == email);

            if (user.RoleId == 1 && currentAdmin.RoleId == 2)
            {
                throw new InvalidOperationException("Admin can't get 'SuperAdmin'.");
            }

            return user;
        }

        public User GetById(int id, User currentAdmin)
        {
            var user = _adminRep.GetAll().FirstOrDefault(u => u.Id == id);

            if (user.RoleId == 1 && currentAdmin.RoleId == 2)
            {
                throw new InvalidOperationException("Admin can't get 'SuperAdmin'.");
            }

            return user;
        }

        public List<User> GetClients()
        {
            return _adminRep.GetAll().Where(u => u.RoleId == 3 || u.RoleId == 2).ToList();
        }

        public List<User> GetProducts()
        {
            return _adminRep.GetAll().Where(u => u.RoleId == 3 || u.RoleId == 2).ToList();
        }

        public bool IsLogin(string Email, string Password)
        {
            bool hasAcc = true;
            var tempObj = _adminRep.GetAll().Where(x => x.Email == Email).FirstOrDefault();

            if (tempObj == null)
            {
                hasAcc = false;
                //return false;
            }
            else
            {
                Byte[] passUser = hash(Password, tempObj.Salt.ToString());
                hasAcc = true;
                if (Email == tempObj.Email) // tempObj.Password == Password
                {
                    for (int i = 0; i < passUser.Length; i++)
                    {
                        if (passUser[i] != tempObj.Password[i])
                        {
                            hasAcc = false;
                        }
                    }
                    //return true;
                }

            }
            return hasAcc;
        }

        public void ReadFromDataBase()
        {
            throw new NotImplementedException();
        }

        public void UpdateName(int id, string firstName, User currentAdmin)
        {
            if (currentAdmin.RoleId != 2) 
            {
                throw new InvalidOperationException("Тільки адміністратор може оновлювати імена користувачів.");
            }

            var admin = _adminRep.GetById(id);
            admin.FirstName = firstName;
            _adminRep.Update(id, admin);
        }

        public void UpdateStatus(int id, int statusid, User currentAdmin)
        {
            if (currentAdmin.RoleId != 2) 
            {
                throw new InvalidOperationException("Тільки адміністратор може оновлювати імена користувачів.");
            }
            var admin = _adminRep.GetById(id);
            admin.StatusId = statusid;
            _adminRep.Update(id, admin);
        }
        private byte[] hash(string pass, string salt)
        {
            var algorithm = SHA512.Create();
            return algorithm.ComputeHash(Encoding.UTF8.GetBytes(pass + salt));
        }
    }
}
