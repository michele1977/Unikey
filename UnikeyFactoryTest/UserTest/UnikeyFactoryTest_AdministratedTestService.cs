using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnikeyFactoryTest.Repository;
using UnikeyFactoryTest.Service;

namespace UserTest
{
    [TestClass]
    public class UnikeyFactoryTest_AdministratedTestService
    {
        [TestMethod]
        public async Task AdministratedTestService_Next_OK()
        {
            AdministratedTestService service = new AdministratedTestService(new AdministratedTestRepository());
            var actualTest = await service.GetAdministratedTestById(1349);
            var firstQuestion = actualTest.AdministratedQuestions[0];
            var nextQuestion = service.Next(actualTest, firstQuestion.Position + 1);

            Assert.AreEqual(actualTest.AdministratedQuestions[1], nextQuestion);
        }

        [TestMethod]
        public async Task AdministratedTestService_Previous_OK()
        {
            AdministratedTestService service = new AdministratedTestService(new AdministratedTestRepository());
            var actualTest = await service.GetAdministratedTestById(1349);
            var secondQuestion = actualTest.AdministratedQuestions[1];
            var previousQuestion = service.Previous(actualTest, secondQuestion.Position - 1);

            Assert.AreEqual(actualTest.AdministratedQuestions[0], previousQuestion);
        }
    }
}
