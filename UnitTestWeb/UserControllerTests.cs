using AutoMapper;
using BLL.IServices;
using DTO.Model;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApplication1.Controllers;
using WebApplication1.Models;
using NUnit.Framework;
using BLL.Services;

namespace UnitTestWeb
{
    public class UserControllerTests
    {
        private Mock<IServiceSuperAdmin> _mockService;
        private Mock<IMapper> _mockMapper;
        private UserController _controller;
        private IServiceSuperAdmin _serviceSuperAdmin;

        [SetUp]
        public void SetUp()
        {
            _serviceSuperAdmin = new ServiceSuperAdmin();
            _mockService = new Mock<IServiceSuperAdmin>();
            _mockMapper = new Mock<IMapper>();
            _controller = new UserController(_mockService.Object, _mockMapper.Object);
        }

        [Test]
        public void GetUser_List_AreEqual()
        {
            User user11 = _serviceSuperAdmin.GetById(1);
            User user22 = _serviceSuperAdmin.GetById(2);

            string pass1 = "yura123";
            string pass2 = "maria123";

            byte[] curpass1 = User.hash(pass1, user11.Salt.ToString());
            byte[] curpass2 = User.hash(pass2, user22.Salt.ToString());
            // Arrange
            var user1 = new UserDto { Id = 1, FirstName = "Yura", LastName = "Hartovanets", Email = "y@gmail.com", Password = curpass1.ToString(), RoleId = 1, StatusId = 1, CreatedAt = DateTime.Parse("2024-10-31 20:22:09.337"), UpdatedAt = DateTime.Parse("2024-10-31 20:22:09.340")};
            var user2 = new UserDto { Id = 2, FirstName = "Mariia",LastName = "Hartovanets", Email = "m@gmail.com", Password = curpass2.ToString(), RoleId = 2, StatusId = 1, CreatedAt = DateTime.Parse("2024-10-31 20:23:07.353"), UpdatedAt = DateTime.Parse("2024-10-31 20:23:07.357")};

            var expectedUsers = new List<UserDto> { user1, user2 };

            _mockService.Setup(x => x.GetProducts()).Returns(new List<User> { new User(), new User() });
            _mockMapper.Setup(m => m.Map<List<UserDto>>(It.IsAny<List<User>>())).Returns(expectedUsers);

            // Act
            var result = _controller.Index() as ViewResult;

            var model = result.Model as List<UserDto>;
            Assert.Equals(expectedUsers.Count, model.Count); 
            for (int i = 0; i < expectedUsers.Count; i++)
            {
                Assert.Equals(expectedUsers[i].FirstName, model[i].FirstName);
                Assert.Equals(expectedUsers[i].LastName, model[i].LastName);
                Assert.Equals(expectedUsers[i].Email, model[i].Email);
            }
        }
        
    }
}
