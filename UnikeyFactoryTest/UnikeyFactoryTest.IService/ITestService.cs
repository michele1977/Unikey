using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnikeyFactoryTest.Domain;
using UnikeyFactoryTest.Domain.Enums;

namespace UnikeyFactoryTest.IService
{
    public interface ITestService
    {
        void AddNewTest(TestBusiness test);
        Task <TestBusiness> GetTestById(int testId);
        Task<List<TestBusiness>> GetTests();
        Task DeleteTest(int testId);
        Task UpdateTest(TestBusiness test);
        Task UpdateQuestion(QuestionBusiness updateQuestion);
        string GenerateUrl(string guid);
        Task<TestBusiness> GetTestByURL(string modelUrl);
        Task DeleteQuestionByIdFromTest(int questionId);
        Task<List<TestBusiness>> GetTestsByFilter(string filter);
        Task<QuestionBusiness> GetQuestionById(int id);
        Task<Dictionary<int, int>> GetExTestCountByState(IEnumerable<int> testsIds, AdministratedTestState state);
        Task AddOrUpdateQuestion(QuestionBusiness question);
        void Dispose();
        StringBuilder TextBuilder(QuestionBusiness question, StringBuilder sb, int position);
        void ClipBoardMethod(StringBuilder sb);
        Task<int> GetExTestCountByState(int testId, AdministratedTestState state);
        Task<int> GetExTestCount(int testId);
        Task<List<TestBusiness>> GetAllFiltered(int pageNum, int pageSize, string filter);
        Task<int> CountTests(string filter);
        Task<TestBusiness> GetTestByIdLight(int testId);
    }
}