using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnikeyFactoryTest.Context;
using UnikeyFactoryTest.Repository;

namespace UnikeyFactoryTest.Service
{
    public class AnswerService
    {
        public void AddNewAnswers(List<Answer> answers)
        {
            //using (AnswerRepository _repo = new AnswerRepository())
            //{
            //    foreach (var answer in answers)
            //    {
            //        if(string.IsNullOrWhiteSpace(answer.Text)) throw new Exception("Answers not saved");
            //    }
            //    _repo.SaveAnswers(answers);
            //}
        }
    }
}
