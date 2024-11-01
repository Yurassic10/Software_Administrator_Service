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
    public class Statuses : IRepository<Status>
    {
        List<Status> _statuses;
        //private readonly SqlConnection connection;
        private readonly string connectionStr; // readonly

        public Statuses(string test1="")
        {
            _statuses = new List<Status>();
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
            _statuses.Clear();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionStr))
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        connection.Open();
                        command.CommandText = "Select * from Statuses";
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            Status temp_status = new Status();
                            temp_status.Id = (int)reader["Id"];
                            temp_status.Name = (string)reader["Status"];

                            _statuses.Add(temp_status);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error during read from databased: {ex.Message}");
            }
        }

        public List<Status> GetAll()
        {
            return _statuses;
        }

        public Status GetById(int id)
        {
            var tempObj = _statuses.Where(x => x.Id == id).SingleOrDefault();
            return tempObj;
        }



        public void Update(int id, Status obj)
        {
            throw new NotImplementedException();
        }









        public void DeleteById(int id)
        {
            throw new NotImplementedException();
        }
        public void Add(Status tempObj)
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

        public Status GetByEmail(string email)
        {
            throw new NotImplementedException();
        }
    }
}
