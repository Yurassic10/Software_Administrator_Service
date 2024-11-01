using BLL.IServices;
using DataAccessLogic.ADO;
using DataAccessLogic.Interfaces;
using DTO.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ServiceRole : IServiceRole
    {
        readonly private IRepository<Role> _roleRep;
        public ServiceRole()
        {
            _roleRep = new Roles();
        }
        public Role GetById(int id)
        {
             return _roleRep.GetById(id);
        }

        public List<Role> GetProducts()
        {
            return _roleRep.GetAll();
        }
    }
}
