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
        Test GetTest(int testId);
        List<TestBusiness> GetTests();
        void UpdateTest(TestBusiness test);
        void DeleteTest(int testId);
    } 
}
