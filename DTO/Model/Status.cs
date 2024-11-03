using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Model
{
    public class Status
    {
        public int Id {  get; set; }
        public string Name { get; set; }

        public bool Equals(Status obj)
        {
            return obj.Id == Id && obj.Name == Name;
        }
    }
}
