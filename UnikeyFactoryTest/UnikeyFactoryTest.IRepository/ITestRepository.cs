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
        TestBusiness GetTestByURL(string URL);
        Task <Test> GetTest(int testId);
        Task<List<TestBusiness>> GetTests();
        void UpdateTest(TestBusiness test);
        Task DeleteTest(int testId);
    } 
}
