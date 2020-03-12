using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnikeyFactoryTest.Context;
using UnikeyFactoryTest.Domain;
using UnikeyFactoryTest.Domain.Enums;

namespace UnikeyFactoryTest.IRepository
{
    public interface ITestRepository : IDisposable
    {
        bool IsContextNull { get; }
        void SaveTest(Test test);
        Task<TestBusiness> GetTestByURL(string URL);
        Task <TestBusiness> GetTest(int testId);
        Task<List<TestBusiness>> GetTests();
        Task UpdateTest(TestBusiness test);
        Task UpdateQuestion(QuestionBusiness updateQuestion);
        Task DeleteTest(int testId);
        Task DeleteQuestionByIdFromTest(int questionId);
        Task<QuestionBusiness> GetQuestionById(int id);
        Task<Dictionary<int, int>> GetExTestCountByState(IEnumerable<int> testsIds, AdministratedTestState state);
        Task<int> GetExTestCountByState(int testId, AdministratedTestState state);
        Task<int> GetExTestCount(int testId);
        Task<List<TestBusiness>> GetAllFiltered(int pageNum, int pageSize, string filter);
        Task<List<TestBusiness>> GetAllFiltered(int pageNum, int pageSize);
        Task<int> CountTests(string filter);
        Task<int> CountTests();
    } 

}
