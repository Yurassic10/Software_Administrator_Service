using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Model
{
    public class Activity
    {
        public int Id {  get; set; }
        public int AdminId{ get; set; }  
        public int UserId { get; set; }
        public int ActionId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
