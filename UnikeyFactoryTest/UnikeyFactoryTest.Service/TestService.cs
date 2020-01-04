using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnikeyFactoryTest.Context;
using UnikeyFactoryTest.Domain;
using UnikeyFactoryTest.Mapper;
using UnikeyFactoryTest.Repository;

namespace UnikeyFactoryTest.Service
{
    public class TestService
    {
        public void AddNewTest(TestBusiness test)
        {
            using (TestRepository _repo = new TestRepository())
            {
                if(string.IsNullOrWhiteSpace(test.URL)) throw new Exception("Test not saved");
                _repo.SaveTest(TestMapper.MapBizToDal(test));
            }
        }
        public TestBusiness GetTestById(string URL)
        {
            using (TestRepository _repo = new TestRepository())
            {
                return _repo.GetTestByURL(URL);
            }
        }
    }
}
