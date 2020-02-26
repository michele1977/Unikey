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
        string GenerateGuid();
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
    }
}