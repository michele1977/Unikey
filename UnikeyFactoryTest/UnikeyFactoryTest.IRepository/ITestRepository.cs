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
        Test GetTest(int testId);
        IEnumerable<Test> GetTests();
        void UpdateTest(TestBusiness test);
        void DeleteTest(int testId);
    } 
}
