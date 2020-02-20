using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
<<<<<<< HEAD
using UnikeyFactoryTest.IRepository;
using UnikeyFactoryTest.IService;
=======
using UnikeyFactoryTest.Repository;
>>>>>>> f8a69651fe1ec26ef34f38fb20e2f259e47258ac
using UnikeyFactoryTest.Service;

namespace UserTest
{
    [TestClass]
    public class UnikeyFactoryTest_AdministratedTestService
    {
        private IAdministratedTestRepository services;
        [TestMethod]
        public async Task AdministratedTestService_Next_OK()
        {
<<<<<<< HEAD
            AdministratedTestService service = new AdministratedTestService(services);
=======
            AdministratedTestService service = new AdministratedTestService(new AdministratedTestRepository());
>>>>>>> f8a69651fe1ec26ef34f38fb20e2f259e47258ac
            var actualTest = await service.GetAdministratedTestById(1349);
            var firstQuestion = actualTest.AdministratedQuestions[0];
            var nextQuestion = service.Next(actualTest, firstQuestion.Position + 1);

            Assert.AreEqual(actualTest.AdministratedQuestions[1], nextQuestion);
        }

        [TestMethod]
        public async Task AdministratedTestService_Previous_OK()
        {
<<<<<<< HEAD
            AdministratedTestService service = new AdministratedTestService(services);
=======
            AdministratedTestService service = new AdministratedTestService(new AdministratedTestRepository());
>>>>>>> f8a69651fe1ec26ef34f38fb20e2f259e47258ac
            var actualTest = await service.GetAdministratedTestById(1349);
            var secondQuestion = actualTest.AdministratedQuestions[1];
            var previousQuestion = service.Previous(actualTest, secondQuestion.Position - 1);

            Assert.AreEqual(actualTest.AdministratedQuestions[0], previousQuestion);
        }
    }
}
