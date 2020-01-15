using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task AddNewTest(TestBusiness test)
        {
            using (TestRepository _repo = new TestRepository())
            {
                if(string.IsNullOrWhiteSpace(test.URL)) throw new Exception("Test not saved");
                await _repo.SaveTest(TestMapper.MapBizToDal(test));
            }
        }

        public async Task <TestBusiness> GetTestById(int testId)
        {
            Repo = new TestRepository();

            TestBusiness test = null;

            try
            {
                test = await Repo.GetTest(testId);
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

            return test;
        }

        public async Task<List<TestBusiness>> GetTests()
        {
            Repo = new TestRepository();
            var tests =  Repo.GetTests();
            return await tests;
        }

        public async Task DeleteTest(int testId)
        {
            using (Repo = new TestRepository())
            {
                try
                {
                    await Repo.DeleteTest(testId);
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

        public void UpdateTest(TestBusiness test)
        {
            using (TestRepository _repo = new TestRepository())
            {
                if (string.IsNullOrWhiteSpace(test.URL)) throw new Exception("Test not saved");
                _repo.UpdateTest(test);
            }
        }


        public string GenerateGuid()
        {
            return Guid.NewGuid().ToString();
        }

        public string GenerateUrl(string guid)
        {
            var baseUrl = ConfigurationManager.AppSettings["baseUrl"];
            return $"{baseUrl}ExTest\\TestStart?guid={guid.ToString()}";

        }


        public TestBusiness GetTestByURL(string modelUrl)
        {
            using (TestRepository _repo = new TestRepository())
            {
                return _repo.GetTestByURL(modelUrl);
            }
        }

        public async Task DeleteQuestion(int questionId)
        {
            using (Repo = new TestRepository())
            {
                try
                {
                    await Repo.DeleteQuestion(questionId);
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
