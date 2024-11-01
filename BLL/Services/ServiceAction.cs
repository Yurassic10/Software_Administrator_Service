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
    public class ServiceAction : IServiceAction
    {
        readonly private IRepository<ActionM> _actionRep;
        public ServiceAction()
        {
            _actionRep = new Actions();
        }
        public ActionM GetById(int id)
        {
            return _actionRep.GetById(id);
        }

        public List<ActionM> GetProducts()
        {
            return _actionRep.GetAll();
        }
    }
}
