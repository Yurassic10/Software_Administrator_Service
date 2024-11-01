using DataAccessLogic.ADO;
using DTO.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.IServices
{
    public interface IServiceStatus
    {
        List<Status> GetProducts();
        Status GetById(int id);
    }
}
