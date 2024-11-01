using BLL.IServices;
using DataAccessLogic.ADO;
using DataAccessLogic.Interfaces;
using DTO.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ServiceSuperAdmin : IServiceSuperAdmin
    {
        readonly private IRepository<User> _usersRep;
        public ServiceSuperAdmin()
        {
            _usersRep = new Users();
        }
        public void Add(User tempObj)
        {
            _usersRep.Add(tempObj);
        }

        public void Delete(int id)
        {
            _usersRep.DeleteById(id);
        }

        public User GetByEmail(string email)
        {
            return _usersRep.GetByEmail(email);
        }

        public User GetById(int id)
        {
           return _usersRep.GetById(id);
        }

        public List<User> GetClients()
        {
            var temp = _usersRep.GetAll();
            List<User> clients= new List<User>();
            foreach (var item in temp) 
            {
                if (item.RoleId==3)
                {
                    clients.Add(item);
                }
            }
            return clients;
        }

        public List<User> GetProducts()
        {
            return _usersRep.GetAll();
        }

        public bool IsLogin(string Email, byte[] Password) // byte[]   string
        {
            // Знаходимо користувача за email
            var tempObj = _usersRep.GetAll().Where(x => x.Email == Email).FirstOrDefault();

            if (tempObj == null)
            {
                // Якщо користувач не знайдений, повертаємо false
                return false;
            }

            // Порівнюємо обчислений хеш збереженим у базі даних
            return Password.SequenceEqual(tempObj.Password);
        }


        //public bool IsLogin(string Email, string Password)
        //{
        //    bool hasAcc = true;
        //    var tempObj = _usersRep.GetAll().Where(x => x.Email == Email).FirstOrDefault();

        //    if (tempObj == null)
        //    {
        //        hasAcc = false;
        //        //return false;
        //    }
        //    else
        //    {
        //        Byte[] passUser = hash(Password, tempObj.Salt.ToString());

        //        hasAcc = true;

        //        if (Email == tempObj.Email) // tempObj.Password == Password
        //        {
        //            for (int i = 0; i < passUser.Length; i++)
        //            {
        //                if (passUser[i] != tempObj.Password[i])
        //                {
        //                    hasAcc = false;
        //                }
        //            }
        //            //return true;
        //        }

        //    }
        //    return hasAcc;
        //}
        //public bool IsLogin(string email, string password)
        //{
        //    // Знаходимо користувача за email
        //    var user = _usersRep.GetAll().FirstOrDefault(x => x.Email == email);

        //    if (user == null)
        //    {
        //        return false; // Користувач не знайдений
        //    }

        //    // Хешуємо введений пароль з сіллю з бази даних
        //    var hashedPassword = hash(password, user.Salt.ToString());

        //    // Порівнюємо хеші (введений хеш і збережений)
        //    return hashedPassword.SequenceEqual(user.Password);
        //}

        public void UpdateName(int id,string firstName)
        {
            var user = _usersRep.GetById(id);
            user.FirstName = firstName;
            _usersRep.Update(id, user);
        }
        public void UpdateStatus(int id, int statusid)
        {
            var user = _usersRep.GetById(id);
            user.StatusId=statusid;
            _usersRep.Update(id, user);
        }
        private byte[] hash(string pass, string salt)
        {
            var algorithm = SHA512.Create();
            return algorithm.ComputeHash(Encoding.UTF8.GetBytes(pass + salt));
        }
    }
}
