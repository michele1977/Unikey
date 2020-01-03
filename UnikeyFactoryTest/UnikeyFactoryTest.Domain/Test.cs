using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnikeyFactoryTest.Domain
{
    public class Test
    {
        public Test()
        {
            this.AdministratedTests = new HashSet<AdministratedTest>();
            this.Questions = new HashSet<Question>();
        }

        public int Id { get; set; }
        public string URL { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public int UserId { get; set; }
        public virtual ICollection<AdministratedTest> AdministratedTests { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
        public virtual User User { get; set; }
    }
}
