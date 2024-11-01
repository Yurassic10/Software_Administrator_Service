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
    public class ServiceActivity : IServiceActivity
    {
        readonly private IRepository<Activity> _activityRep;
        public ServiceActivity()
        {
            _activityRep = new Activities();
        }
        public void Add(Activity tempObj)
        {
            _activityRep.Add(tempObj);
        }

        public Activity GetById(int id)
        {
            return _activityRep.GetById(id);
        }

        public List<Activity> GetProducts()
        {
            return _activityRep.GetAll();
        }
    }
}
