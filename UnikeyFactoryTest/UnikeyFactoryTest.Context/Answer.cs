
namespace UnikeyFactoryTest.Context
{
    using System;
    using System.Collections.Generic;
    
    public partial class Answer
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public bool? IsCorrect { get; set; }
        public int QuestionId { get; set; }
        public decimal? Score { get; set; }
    
        public virtual Question Question { get; set; }
    }
}
