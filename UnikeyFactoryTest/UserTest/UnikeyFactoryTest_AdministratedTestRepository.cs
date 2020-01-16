using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnikeyFactoryTest.Repository;

namespace UserTest
{
    [TestClass]
    public class UnikeyFactoryTest_AdministratedTestRepository
    {
        [TestMethod]
        public async Task GetState_OK()
        {
            
            
            AdministratedTestRepository testRepository = new AdministratedTestRepository();
            var x = await testRepository.GetState(2);
            Assert.AreEqual(1,  x);


        }

    }
}
