using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using UnikeyFactoryTest.WebAPI.Controllers;

namespace WebApiNUnit
{
    [TestFixture]
    public class ExTestControllerUnitTest
    {

        [Test]
        public void ExTestController()
        {
            var controller = new ExTestController();
        }

    }
}
