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
    public class Actions : IRepository<ActionM>
    {
        List<ActionM> _actions;
        //private readonly SqlConnection connection;
        private readonly string connectionStr; // readonly

        public Actions(string test1="")
        {
            _actions= new List<ActionM>();
            if(test1=="test")
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
            _actions.Clear();
            try 
            {
                using (SqlConnection connection = new SqlConnection(connectionStr))
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        connection.Open();
                        command.CommandText = "Select * from Actions";
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            ActionM temp_actionM = new ActionM();
                            temp_actionM.Id = (int)reader["Id"];
                            temp_actionM.Name = (string)reader["Action"];

                            _actions.Add(temp_actionM);
                        }
                    }
                }
            } 
            catch (Exception ex)
            {
                throw new Exception($"Error during read from databased: {ex.Message}");
            }
        }
        

        public List<ActionM> GetAll()
        {
            return _actions;
        }

        public ActionM GetById(int id)
        {
            var tempObj = _actions.Where(x => x.Id==id).SingleOrDefault();
            return tempObj;
        }






        public void Update(int id, ActionM obj)
        {
            throw new NotImplementedException();
        }


        public void Add(ActionM tempObj)
        {
            throw new NotImplementedException();
        } 

        public void DeleteById(int id)
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

        public ActionM GetByEmail(string email)
        {
            throw new NotImplementedException();
        }
    }
}
