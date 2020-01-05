using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnikeyFactoryTest.Context;
using UnikeyFactoryTest.Domain;
using UnikeyFactoryTest.IRepository;
using UnikeyFactoryTest.Mapper;
using UnikeyFactoryTest.Repository;

namespace UnikeyFactoryTest.Service
{
    public class TestService 
    {
        public ITestRepository Repo { get; set; }

        public TestService()
        {
            Repo = new TestRepository();
        }

        public void AddNewTest(TestBusiness test)
        {
            using (TestRepository _repo = new TestRepository())
            {
                if(string.IsNullOrWhiteSpace(test.URL)) throw new Exception("Test not saved");
                _repo.SaveTest(TestMapper.MapBizToDal(test));
            }
        }

        public TestBusiness GetTestById(int testId)
        {
            Repo = new TestRepository();

            Test test = null;

            try
            {
                test = Repo.GetTest(testId);
            }
            catch (NullReferenceException ex)
            {
                throw;
            }
            catch (InvalidOperationException ex)
            {
                //TODO
            }
            catch (Exception ex)
            {
                //TODO
            }

            TestBusiness testBusiness = TestMapper.MapDalToBiz(test);

            return testBusiness;

        }

        public IEnumerable<TestBusiness> GetTests()
        {
            Repo = new TestRepository();
            var tests = Repo.GetTests().Select(TestMapper.MapDalToBiz);
            return tests;
        }

        public void DeleteTest(int testId)
        {
            using (Repo = new TestRepository())
            {
                try
                {
                    Repo.DeleteTest(testId);
                }
                catch (NullReferenceException ex)
                {
                    throw;
                }
                catch (NotSupportedException ex)
                {

                }
                catch (ObjectDisposedException ex)
                {

                }
                catch (InvalidOperationException ex)
                {

                }
                catch (Exception ex)
                {

                }
            }
        }
    }
}
