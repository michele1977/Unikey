using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using UnikeyFactoryTest.Context;
using UnikeyFactoryTest.IService;
using UnikeyFactoryTest.Repository;
using UnikeyFactoryTest.Service;
using UserTest.AutoMappers;

namespace UserTest
{
    [TestClass]
    public class UnikeyFactoryTest_AdministratedTestService
    {
        private TestPlatformDBEntities GetContext()
        {
            return new TestPlatformDBEntities();
        }
        private StandardKernel GetKernel()
        {
            return new StandardKernel();
        }
        [TestMethod]
        public async Task AdministratedTestService_Next_OK()
        {
            var kernel = KernelBuilder.Build();
            var service = kernel.Get<IAdministratedTestService>();
            var actualTest = await service.GetAdministratedTestById(1349);
            var firstQuestion = actualTest.AdministratedQuestions[0];
            var nextQuestion = service.Next(actualTest, firstQuestion.Position + 1);

            Assert.AreEqual(actualTest.AdministratedQuestions[1], nextQuestion);
        }

        [TestMethod]
        public async Task AdministratedTestService_Previous_OK()
        {
            var kernel = KernelBuilder.Build();
            var service = kernel.Get<IAdministratedTestService>();
            var actualTest = await service.GetAdministratedTestById(1349);
            var secondQuestion = actualTest.AdministratedQuestions[1];
            var previousQuestion = service.Previous(actualTest, secondQuestion.Position - 1);

            Assert.AreEqual(actualTest.AdministratedQuestions[0], previousQuestion);
        }
    }
}
