using DTO.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.IServices
{
    public interface IServiceAdmin
    {
        //void ReadFromDataBase();
        List<User> GetProducts();
        void Add(User tempObj, User currentAdmin);
        void Delete(int id,User currentAdmin);
        User GetByEmail(string email, User currentAdmin);
        User GetById(int id, User currentAdmin);
        bool IsLogin(string Email, string Password);
        public void UpdateName(int id, string firstName, User currentAdmin);
        public void UpdateStatus(int id, int statusid, User currentAdmin);

        //List<User> GetClients();
    }
}
