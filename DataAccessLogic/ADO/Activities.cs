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
    public class Activities :IRepository<Activity>
    {
        List<Activity> _activities;
        //private readonly SqlConnection connection;
        private readonly string connectionStr; // readonly

        public Activities(string test1 = "")
        {
            _activities = new List<Activity>();
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
            _activities.Clear();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionStr))
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        connection.Open();
                        command.CommandText = "Select * from Activities";
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            Activity temp_activity = new Activity();
                            temp_activity.Id = (int)reader["Id"];
                            temp_activity.AdminId = (int)reader["AdminId"];
                            temp_activity.UserId = (int)reader["UserId"];
                            temp_activity.ActionId = (int)reader["ActionId"];

                            _activities.Add(temp_activity);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error during read from databased: {ex.Message}");
            }
        }


       
        public void Add(Activity tempObj) // Логування дій адміна
        {
            _activities.Add(tempObj);
            using (SqlConnection connection = new SqlConnection(connectionStr))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    connection.Open();

                    command.CommandText = "Insert into Activities (AdminId,UserId,ActionId,CreatedAt) VALUES (@adminId,@userId,@actionId,@createdAt)";
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@adminId", tempObj.AdminId);
                    command.Parameters.AddWithValue("@userId", tempObj.UserId);
                    command.Parameters.AddWithValue("@actionId", tempObj.ActionId);
                    command.Parameters.AddWithValue("@createdAt", tempObj.CreatedAt);
                    command.ExecuteNonQuery();

                    //connection.Close();
                }
            }
        }
        public List<Activity> GetAll()
        {
            return _activities;
        }
        public Activity GetById(int id)
        {
            var tempObj = _activities.Where(x => x.Id == id).SingleOrDefault();
            return tempObj;
        }

        public void Update(int id, Activity obj)
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

        public Activity GetByEmail(string email)
        {
            throw new NotImplementedException();
        }


        /*
       public bool LogActivity(int adminId, int userId, int actionId)
       {
           bool Added = true;
           try
           {
               using (SqlCommand command = connection.CreateCommand())
               {
                   command.CommandText = "INSERT INTO tblAdminUserActivity (AdminID, UserID, ActionId, CreatedAt) VALUES (@adminId, @userId, @actionId, @createdAt)";
                   command.Parameters.AddWithValue("@adminId", adminId);
                   command.Parameters.AddWithValue("@userId", userId);
                   command.Parameters.AddWithValue("@actionId", actionId);
                   command.Parameters.AddWithValue("@createdAt", DateTime.Now);
                   connection.Open();
                   command.ExecuteNonQuery();
                   connection.Close();
               }
           }
           catch (Exception ex)
           {
               Added = false;
           }
           return Added;
       }
       */
    }
}
