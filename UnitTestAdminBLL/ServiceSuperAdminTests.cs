using BLL.Services;
using DataAccessLogic.ADO;
using DTO.Model;
using Moq;

namespace UnitTestAdminBLL
{
    public class ServiceSuperAdminTests
    {
        private ServiceSuperAdmin _serviceSuperAdmin;
        [SetUp]
        public void Setup()
        {
            _serviceSuperAdmin = new ServiceSuperAdmin();
        }

        [Test]
        public void GetUser_ById_AreEqual()
        {
            DateTime createdAt = DateTime.Now;
            DateTime updatedAt = DateTime.Now;
            Guid salt = Guid.Empty;

            string pass = "nazar123";
            byte[] curpass = User.hash(pass, salt.ToString());

            // act
            User resultUser = _serviceSuperAdmin.GetById(5);

            User expectedUser = new User
            {
                Id = 5,
                FirstName = "Nazar",
                LastName = "Petrenko",
                Email = "n@gmail.com",
                Password = curpass,
                Salt = resultUser.Salt,
                RoleId = 3,
                StatusId = 1,
                CreatedAt = Convert.ToDateTime("2024-10-31 20:28:44.563"),
                UpdatedAt = Convert.ToDateTime("2024 - 10 - 31 20:28:44.563")
            };                     

            // assert
            Assert.IsTrue(expectedUser.Equals(resultUser));
        }
        [Test]
        public void GetUser_List_AreEqual() 
        {
            User user1 = _serviceSuperAdmin.GetById(1);
            User user2 = _serviceSuperAdmin.GetById(2);
            string pass1 = "yura123";
            string pass2 = "maria123";
            Guid salt = Guid.Empty;

            byte[] curpass1 = User.hash(pass1, user1.Salt.ToString());
            byte[] curpass2 = User.hash(pass2, user2.Salt.ToString());

            // Expected
            List<User> expectedUsers = new List<User>()
            {
                new User { Id = 1, FirstName = "Yura", LastName = "Hartovanets", Email = "y@gmail.com", Password = curpass1, Salt = user1.Salt, RoleId = 1, StatusId = 1, CreatedAt = DateTime.Parse("2024-10-31 20:22:09.337"), UpdatedAt = DateTime.Parse("2024-10-31 20:22:09.340") },
                new User { Id = 2, FirstName = "Mariia", LastName = "Hartovanets", Email = "m@gmail.com", Password = curpass2, Salt = user2.Salt, RoleId = 2, StatusId = 1, CreatedAt = Convert.ToDateTime("2024-10-31 20:23:07.353"), UpdatedAt = Convert.ToDateTime("2024-10-31 20:23:07.357") }

            };

            // Act
            List<User> resultUsers = _serviceSuperAdmin.GetProducts();

            // Assert
            for (int i = 0; i < expectedUsers.Count; i++)
            {
                Assert.IsTrue(expectedUsers[i].Equals(resultUsers[i]));
            }
        }
        [Test]
        public void GetUser_ListCount_AreEqual()
        {
            var users = _serviceSuperAdmin.GetProducts();
            string pass1 = "yura123";
            string pass2 = "maria123";
            string pass3 = "stas123";
            string pass4 = "roman123";
            string pass5 = "nazar123";


            byte[] curpass1 = User.hash(pass1, users[0].Salt.ToString());
            byte[] curpass2 = User.hash(pass2, users[1].Salt.ToString());
            byte[] curpass3 = User.hash(pass3, users[2].Salt.ToString());
            byte[] curpass4 = User.hash(pass4, users[3].Salt.ToString());
            byte[] curpass5 = User.hash(pass5, users[4].Salt.ToString());

            // expected
            List<User> expectedUsers = new List<User>()
            {
                new User { Id = 1, FirstName = "Yura", LastName = "Hartovanets", Email = "y@gmail.com", Password = curpass1, Salt = users[0].Salt, RoleId = 1, StatusId = 1, CreatedAt = DateTime.Parse("2024-10-31 20:22:09.337"), UpdatedAt = DateTime.Parse("2024-10-31 20:22:09.340") },
                new User { Id = 2, FirstName = "Mariia", LastName = "Hartovanets", Email = "m@gmail.com", Password = curpass2, Salt = users[1].Salt, RoleId = 2, StatusId = 1, CreatedAt = Convert.ToDateTime("2024-10-31 20:23:07.353"), UpdatedAt = Convert.ToDateTime("2024-10-31 20:23:07.357") },
                new User { Id = 3, FirstName = "Stas", LastName = "Tymniak", Email = "t@gmail.com", Password = curpass3, Salt = users[2].Salt, RoleId = 2, StatusId = 1, CreatedAt = Convert.ToDateTime("2024-10-31 20:25:28.560"), UpdatedAt = Convert.ToDateTime("2024-10-31 20:25:28.563") },
                new User { Id = 4, FirstName = "Roman", LastName = "Shtay", Email = "r@gmail.com", Password = curpass4, Salt = users[3].Salt, RoleId = 3, StatusId = 1, CreatedAt = Convert.ToDateTime("2024-10-31 20:27:28.013"), UpdatedAt = Convert.ToDateTime("2024-10-31 20:27:28.017") },
                new User { Id = 5, FirstName = "Nazar", LastName = "Petrenko", Email = "n@gmail.com", Password = curpass5, Salt = users[4].Salt, RoleId = 3, StatusId = 1, CreatedAt = Convert.ToDateTime("2024-10-31 20:28:44.563"), UpdatedAt = Convert.ToDateTime("2024-10-31 20:28:44.563") }
                
            };


            // act
            List<User> resultUsers = _serviceSuperAdmin.GetProducts();

            // assert
            Assert.AreEqual(expectedUsers.Count, resultUsers.Count);
        }
    }
}