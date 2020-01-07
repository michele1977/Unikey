using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnikeyFactoryTest.Domain
{
    public class QuestionBusiness
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int TestId { get; set; }

        public List<AnswerBusiness> Answers { get; set; }
    }
}