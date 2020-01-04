using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnikeyFactoryTest.Context;
using UnikeyFactoryTest.Domain;

namespace UnikeyFactoryTest.IRepository
{
    public interface ITestRepository
    {
        void SaveTest(Test test);
        void SaveQuestion(Question question);
        void SaveAnswers(List<Answer> answers);
        TestBusiness GetTestByURL(string URL);
    } 
}
