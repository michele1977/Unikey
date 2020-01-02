using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnikeyFactoryTest.Domain
{
    public class AdministratedTest
    {
        public AdministratedTest()
        {
            this.AdministratedQuestions = new HashSet<AdministratedQuestion>();
        }
        public int Id { get; set; }
        public string URL { get; set; }
        public string Text { get; set; }
        public Nullable<int> TotalScore { get; set; }
        public int TestId { get; set; }
        public string TestSubject { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public virtual ICollection<AdministratedQuestion> AdministratedQuestions { get; set; }
        public virtual Test Test { get; set; }
    }
}
