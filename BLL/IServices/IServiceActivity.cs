using DTO.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.IServices
{
    public interface IServiceActivity
    {
        void Add(Activity tempObj);
        List<Activity> GetProducts();
        Activity GetById(int id);
    }
}
