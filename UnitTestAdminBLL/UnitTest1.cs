using BLL.Services;

namespace UnitTestAdminBLL
{
    public class Tests
    {
        private ServiceSuperAdmin serviceSuperAdmin;
        [SetUp]
        public void Setup()
        {
            serviceSuperAdmin = new ServiceSuperAdmin();
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}