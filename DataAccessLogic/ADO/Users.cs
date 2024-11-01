using DataAccessLogic.Interfaces;
using DTO.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace DataAccessLogic.ADO
{
    public class Users : IRepository<User>
    {
        List<User> _users;
        //private readonly SqlConnection connection;
        private readonly string connectionStr; // readonly

        public Users(string test1 = "")
        {
            _users = new List<User>();
            if (test1 == "test")
            {
                connectionStr = "Data Source=DESKTOP-NALH133;Initial Catalog=AdminServiceTest;Integrated Security=True;Trust Server Certificate=True";
            }
            else
            {
                connectionStr = "Data Source=DESKTOP-NALH133;Initial Catalog=AdminService;Integrated Security=True;TrustServerCertificate=True";
            }
            //Data Source=DESKTOP-NALH133;Initial Catalog=AdminService;Integrated Security=True;Trust Server Certificate=True
            //connection = new SqlConnection(connectionStr);
            ReadFromDataBase();
        }

        private void ReadFromDataBase()
        {
            _users.Clear();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionStr))
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        connection.Open();
                        command.CommandText = "Select * from Users";
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            User temp_user = new User();
                            temp_user.Id = (int)reader["Id"];
                            temp_user.FirstName = (string)reader["FirstName"];
                            temp_user.LastName = (string)reader["LastName"];
                            temp_user.Email = (string)reader["Email"];
                            temp_user.Password = (byte[])reader["Password"];
                            temp_user.Salt = (Guid)reader["Salt"];
                            temp_user.RoleId = (int)reader["RoleId"];
                            temp_user.StatusId = (int)reader["StatusId"];
                            var timeCreatedAt = reader["CreatedAt"];
                            temp_user.CreatedAt = (DateTime)timeCreatedAt;
                            var timeUpdate = reader["UpdatedAt"];
                            temp_user.UpdatedAt = (DateTime)timeUpdate;

                            _users.Add(temp_user);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error during read from databased: {ex.Message}");
            }
        }

        public void Add(User tempObj)
        {
            _users.Add(tempObj);
            using (SqlConnection connection = new SqlConnection(connectionStr))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = "Insert into Users(FirstName,LastName,Email,Password,Salt,RoleId,StatusId,CreatedAt,UpdatedAt)"
                        + " VALUES(@firstName,@lastName,@email,@password,@salt,@roleId,@statusId,@timeCreated,@timeUpdated)";

                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@firstName", tempObj.FirstName);
                    command.Parameters.AddWithValue("@lastName", tempObj.LastName);
                    command.Parameters.AddWithValue("@email", tempObj.Email);
                    command.Parameters.AddWithValue("@password", tempObj.Password);
                    command.Parameters.AddWithValue("@salt",tempObj.Salt); // треба додати в таблицю Users в БД
                    command.Parameters.AddWithValue("@roleId", tempObj.RoleId);
                    command.Parameters.AddWithValue("@statusId", tempObj.StatusId);
                    command.Parameters.AddWithValue("@timeCreated", tempObj.CreatedAt);
                    command.Parameters.AddWithValue("@timeUpdated", tempObj.UpdatedAt);

                    command.ExecuteNonQuery();
                }
            }
            //connection.Close();
        }

        public void DeleteById(int id)
        {
            var tempObj = _users.Where(x => x.Id == id).FirstOrDefault();
            if (tempObj != null)
            {
                _users.Remove(tempObj);
                using (SqlConnection connection = new SqlConnection(connectionStr))
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        connection.Open();
                        command.CommandText = "Delete from Users Where Id = @id"; // хз чи правильно
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@id", id);// чи Id чи tempObj.Id
                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        public List<User> GetAll()
        {
            return _users;
        }

        public User GetById(int id)
        {
            var tempObj = _users.Where(x => x.Id == id).SingleOrDefault();
            return tempObj;
        }

        public User GetByEmail(string email)
        {
            var tempObj = _users.Where(x => x.Email == email).SingleOrDefault();

            return tempObj;
        }

        public void ActivateUser(int userId)
        {
            UpdateUserStatus(userId, 1); // Enabled
        }

        public void BlockUser(int userId)
        {
            UpdateUserStatus(userId, 2); // Disabled
        }

        public void Update(int id, User obj)
        {
            var tempObj = _users.FirstOrDefault(x => x.Id == id);

            if (tempObj != null)
            {
                tempObj.FirstName = obj.FirstName;
                tempObj.LastName = obj.LastName;
                tempObj.Email = obj.Email;
                tempObj.Password = obj.Password;
                tempObj.RoleId = obj.RoleId;
                tempObj.StatusId = obj.StatusId;
                //tempObj.CreatedAt= obj.CreatedAt;
                tempObj.UpdatedAt = DateTime.Now; 


                using (SqlConnection connection = new SqlConnection(connectionStr))
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        connection.Open();
                        command.CommandText = "UPDATE Users SET FirstName = @firstName, LastName = @lastName, Email = @email, " +
                            "Password = @password, RoleId = @roleId, StatusId = @statusId, UpdatedAt = @updatedAt WHERE Id = @id";

                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@firstName", obj.FirstName);
                        command.Parameters.AddWithValue("@lastName", obj.LastName);
                        command.Parameters.AddWithValue("@email", obj.Email);
                        command.Parameters.AddWithValue("@password", obj.Password);
                        command.Parameters.AddWithValue("@roleId", obj.RoleId);
                        command.Parameters.AddWithValue("@statusId", obj.StatusId);
                        command.Parameters.AddWithValue("@updatedAt", DateTime.Now);
                        command.Parameters.AddWithValue("@id", id);

                        command.ExecuteNonQuery();
                    }
                }
            }
            else
            {
                throw new Exception("User not found.");
            }
        }


        public void UpdateUserStatus(int userId, int status)
        {
            var tempObj = _users.FirstOrDefault(x => x.Id == userId);
            if (tempObj != null)
            {
                tempObj.StatusId = status;
                using (SqlConnection connection = new SqlConnection(connectionStr))
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        connection.Open();
                        command.CommandText = "UPDATE Users SET StatusId = @statusId WHERE Id = @userId";
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@statusId", status);
                        command.Parameters.AddWithValue("@userId", userId);
                        command.ExecuteNonQuery();
                    }
                }
            }
        }
        private byte[] hash(string pass, string salt)
        {
            var algorithm = SHA512.Create();
            return algorithm.ComputeHash(Encoding.UTF8.GetBytes(pass + salt));
        }
    }
}
