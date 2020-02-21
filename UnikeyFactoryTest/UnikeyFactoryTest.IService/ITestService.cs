using System.Collections.Generic;
using System.Threading.Tasks;
using UnikeyFactoryTest.Domain;

namespace UnikeyFactoryTest.IService
{
    public interface ITestService
    {
        Task AddNewTest(TestBusiness test);
        Task <TestBusiness> GetTestById(int testId);
        Task<List<TestBusiness>> GetTests();
        Task DeleteTest(int testId);
        void UpdateTest(TestBusiness test);
        string GenerateGuid();
        string GenerateUrl(string guid);
        TestBusiness GetTestByURL(string modelUrl);
        Task DeleteQuestionByIdFromTest(int questionId);
        Task<List<TestBusiness>> GetTestsByFilter(string filter);
        Task<QuestionBusiness> GetQuestionById(int id);
        Task<Dictionary<int, int>> OpenedTestNumber(List<int> TestsId);
        Dictionary<int, int> GetClosedTests(int pageNum, int pageSize, string filter);
        void Dispose();
    }
}