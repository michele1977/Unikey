using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnikeyFactoryTest.Context;

namespace UnikeyFactoryTest.IRepository
{
    public interface ITestRepository
    {
        void AddQuestions(Test test, List<Question> questions);
        void SaveTest(Test test);
    }
}
