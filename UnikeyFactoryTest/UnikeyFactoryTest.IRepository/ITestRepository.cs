using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnikeyFactoryTest.Context;
using UnikeyFactoryTest.Domain;

namespace UnikeyFactoryTest.IRepository
{
    public interface ITestRepository : IDisposable
    {
        void SaveTest(Test test);
        Task<TestBusiness> GetTestByURL(string URL);
        Task <TestBusiness> GetTest(int testId);
        Task<List<TestBusiness>> GetTests();
        Task UpdateTest(TestBusiness test);
        Task UpdateQuestion(QuestionBusiness updateQuestion);
        Task DeleteTest(int testId);
        Task DeleteQuestionByIdFromTest(int questionId);
        Task<QuestionBusiness> GetQuestionById(int id);
        Task<Dictionary<int, int>> OpenedTestNumber(IEnumerable<int> TestsId);
        Task<Dictionary<int, int>> GetClosedTests(int pageNum, int pageSize, string filter);
    } 

}
