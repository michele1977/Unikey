using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnikeyFactoryTest.Context;
using UnikeyFactoryTest.Repository;

namespace UnikeyFactoryTest.Service
{
    public class TestService
    {
        public void AddNewTest(Test test)
        {
            using (TestRepository _repo = new TestRepository())
            {
                if(string.IsNullOrWhiteSpace(test.URL)) throw new Exception("Test not saved");
                _repo.SaveTest(test);
            }
        }
    }
}
