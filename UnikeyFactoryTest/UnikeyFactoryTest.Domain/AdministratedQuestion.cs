using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnikeyFactoryTest.Domain
{
    public class AdministratedQuestion
    {
        public AdministratedQuestion()
        {
            this.AdministratedAnswers = new HashSet<AdministratedAnswer>();
        }
        public int Id { get; set; }
        public string Text { get; set; }
        public int AdministratedTestId { get; set; }
        public virtual ICollection<AdministratedAnswer> AdministratedAnswers { get; set; }
        public virtual AdministratedTest AdministratedTest { get; set; }
    }
}
