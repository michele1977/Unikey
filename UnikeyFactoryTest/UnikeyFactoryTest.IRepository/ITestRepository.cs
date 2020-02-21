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
        Task SaveTest(Test test);
        TestBusiness GetTestByURL(string URL);
        Task <TestBusiness> GetTest(int testId);
        Task<List<TestBusiness>> GetTests();
        void UpdateTest(TestBusiness test);
        Task DeleteTest(int testId);
        Task DeleteQuestionByIdFromTest(int questionId);
        Task<QuestionBusiness> GetQuestionById(int id);
        Task<Dictionary<int, int>> OpenedTestNumber(IEnumerable<int> TestsId);
        Dictionary<int, int> GetClosedTests(int pageNum, int pageSize, string filter);
    } 

}
