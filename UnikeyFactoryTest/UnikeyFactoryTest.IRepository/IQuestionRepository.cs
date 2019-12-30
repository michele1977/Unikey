using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnikeyFactoryTest.Context;

namespace UnikeyFactoryTest.IRepository
{
    public interface IQuestionRepository
    {
        void SaveQuestions(List<Question> questions);
    }
}
