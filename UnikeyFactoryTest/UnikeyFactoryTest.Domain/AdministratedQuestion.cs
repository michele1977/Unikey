using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnikeyFactoryTest.Domain
{
    public class AdministratedQuestion
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int AdministratedTestId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AdministratedAnswer> AdministratedAnswers { get; set; }
        public virtual AdministratedTest AdministratedTest { get; set; }
    }
}
