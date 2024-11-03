using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Model
{
    public class Role
    {
        public int Id {  get; set; }
        public string Name { get; set; }

        public bool Equals(Role obj)
        {
            return obj.Id == Id && obj.Name == Name;
        }

    }
}
