using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Model
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public byte[] Password { get; set; }
        public Guid Salt { get; set; }
        public int RoleId { get; set; }
        public int StatusId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }


        public bool Equals(User obj)
        {
            return obj != null
                && obj.Id == this.Id
                && obj.FirstName == this.FirstName
                && obj.LastName == this.LastName
                && obj.Email == this.Email
                && obj.RoleId == this.RoleId
                && obj.StatusId == this.StatusId
                && obj.CreatedAt == this.CreatedAt
                && obj.UpdatedAt == this.UpdatedAt;
        }

    }
}
