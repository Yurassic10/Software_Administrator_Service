using DataAccessLogic.Interfaces;
using DTO.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLogic.ADO
{
    public class Roles : IRepository<Role>
    {
        List<Role> _roles;
        //private readonly SqlConnection connection;
        private readonly string connectionStr; // readonly

        public Roles(string test1 = "")
        {
            _roles = new List<Role>();
            if (test1 == "test")
            {
                connectionStr = "Data Source=DESKTOP-NALH133;Initial Catalog=AdminServiceTest;Integrated Security=True;Trust Server Certificate=True";
            }
            else
            {
                connectionStr = "Data Source=DESKTOP-NALH133;Initial Catalog=AdminService;Integrated Security=True;TrustServerCertificate=True";
            }
            //connection = new SqlConnection(connectionStr);
            ReadFromDataBase();
        }
        public void ReadFromDataBase()
        {
            _roles.Clear();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionStr))
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        connection.Open();
                        command.CommandText = "Select * from Roles";
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            Role temp_role = new Role();
                            temp_role.Id = (int)reader["Id"];
                            temp_role.Name = (string)reader["Role"];

                            _roles.Add(temp_role);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error during read from databased: {ex.Message}");
            }
        }

        public List<Role> GetAll()
        {
            return _roles;
        }

        public Role GetById(int id)
        {
            var temObj = _roles.Where(x => x.Id == id).SingleOrDefault();
            return temObj;
        }


        public void Update(int id, Role obj)
        {
            throw new NotImplementedException();
        }







        public void DeleteById(int id)
        {
            throw new NotImplementedException();
        }
        public void Add(Role tempObj)
        {
            throw new NotImplementedException();
        }

        public void ActivateUser(int userId)
        {
            throw new NotImplementedException();
        }

        public void BlockUser(int userId)
        {
            throw new NotImplementedException();
        }

        public Role GetByEmail(string email)
        {
            throw new NotImplementedException();
        }
    }
}
