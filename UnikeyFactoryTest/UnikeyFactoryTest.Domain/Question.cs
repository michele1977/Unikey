using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnikeyFactoryTest.Domain
{
    public class Question
    {

        public Question()
        {
            this.Answers = new HashSet<Answer>();
        }

        public int Id { get; set; }
        public string Text { get; set; }
        public int TestId { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }
        public virtual Test Test { get; set; }
    }
}
