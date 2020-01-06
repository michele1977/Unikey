using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnikeyFactoryTest.Domain
{
    public class AdministratedAnswerBusiness
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public bool? isCorrect { get; set; }
        public bool? isSelected { get; set; }
        public int AdministratedQuestionId { get; set; }
        public decimal? Score { get; set; }
        public virtual AdministratedQuestionBusiness AdministratedQuestion { get; set; }
    }
}
