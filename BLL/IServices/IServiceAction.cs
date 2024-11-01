using DTO.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.IServices
{
    public interface IServiceAction
    {
        List<ActionM> GetProducts();
        ActionM GetById(int id);
    }
}
