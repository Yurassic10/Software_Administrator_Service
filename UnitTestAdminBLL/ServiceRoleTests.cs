using BLL.IServices;
using BLL.Services;
using DTO.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestAdminBLL
{
    public class ServiceRoleTests
    {
        private IServiceRole serviceRole;
        [SetUp]
        public void SetUp()
        {
            serviceRole = new ServiceRole();
        }
        [Test]
        public void GetRole_Count_AreEqual()
        {
            // expected
            List<Role> expectedRoles = new List<Role>()
            {
                new Role { Id = 1, Name = "super admin"},
                new Role { Id = 2, Name = "admin"},
                new Role { Id = 3, Name = "client"}

            };

            // act
            List<Role> resultRoles = serviceRole.GetProducts();

            // assert
            Assert.AreEqual(expectedRoles.Count, resultRoles.Count);
        }

        [Test]
        public void GetRole_ById_AreEqual()
        {
            // act
            var resultrole = serviceRole.GetById(1);

            var expectedrole = new Role
            {
                Id = 1,
                Name = "super admin"
            };

            // assert
            Assert.IsTrue(expectedrole.Equals(resultrole));

        }
        [Test]
        public void GetRole_List_AreEqual()
        {

            // expected
            List<Role> expectedRoles = new List<Role>()
            {
                new Role { Id = 1, Name = "super admin"},
                new Role { Id = 2, Name = "admin"},
                new Role { Id = 3, Name = "client"}

            };

            // act
            List<Role> resultRoles = serviceRole.GetProducts();

            // assert
            for (int i = 0; i < expectedRoles.Count; i++)
            {
                Assert.IsTrue(expectedRoles[i].Equals(resultRoles[i]));
            }
        }
    }
}
