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
    public class ServiceStatus : IServiceStatus
    {
        readonly private IRepository<Status> _statusRep;
        public ServiceStatus()
        {
            _statusRep = new Statuses();
        }

        public Status GetById(int id)
        {
            return _statusRep.GetById(id);
        }

        public List<Status> GetProducts()
        {
            return _statusRep.GetAll();
        }
    }
}
