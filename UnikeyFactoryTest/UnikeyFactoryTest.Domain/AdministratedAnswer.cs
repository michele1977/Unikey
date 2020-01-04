using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnikeyFactoryTest.Domain
{
    public class AdministratedAnswer
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public Nullable<bool> isCorrect { get; set; }
        public Nullable<bool> isSelected { get; set; }
        public int AdministratedQuestionId { get; set; }
        public Nullable<decimal> Score { get; set; }
        public virtual AdministratedQuestion AdministratedQuestion { get; set; }
    }
}
