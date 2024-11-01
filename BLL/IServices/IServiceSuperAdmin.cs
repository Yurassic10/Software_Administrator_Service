using DTO.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.IServices
{
    public interface IServiceSuperAdmin // для superadmin
    {
        List<User> GetProducts(); 
        void Add(User tempObj); 
        void Delete(int id); 
        User GetByEmail(string email);
        User GetById(int id);
        bool IsLogin(string Email, byte[] Password); // string
        public void UpdateName(int id,string firstName);
        public void UpdateStatus(int id, int statusid);

       // List<User> GetClients();


    }
}
