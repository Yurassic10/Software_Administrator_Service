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
    public class ServiceStatusTests
    {
        private IServiceStatus serviceStatus;
        [SetUp]
        public void SetUp()
        {
            serviceStatus = new ServiceStatus();
        }
        [Test]
        public void GetStatus_Count_AreEqual()
        {
            // expected
            List<Status> expectedRoles = new List<Status>()
            {
                new Status { Id = 1, Name = "Enabled"},
                new Status { Id = 2, Name = "Disabled"}
            };

            // act
            List<Status> resultStatuses= serviceStatus.GetProducts();

            // assert
            Assert.AreEqual(expectedRoles.Count, resultStatuses.Count);
        }

        [Test]
        public void GetStatus_ById_AreEqual()
        {
            // act
            var resultStatus= serviceStatus.GetById(1);

            var expectedStatus= new Status
            {
                Id = 1,
                Name = "Enabled"
            };

            // assert
            Assert.IsTrue(expectedStatus.Equals(resultStatus));
        }

        [Test]
        public void GetStatus_List_AreEqual()
        {

            // expected
            List<Status> expectedStatus = new List<Status>()
            {
                new Status { Id = 1, Name = "Enabled"},
                new Status { Id = 2, Name = "Disabled"}
            };

            // act
            List<Status> resultStatuses = serviceStatus.GetProducts();

            // assert
            for (int i = 0; i < expectedStatus.Count; i++)
            {
                Assert.IsTrue(expectedStatus[i].Equals(resultStatuses[i]));
            }
        }
    }
}
