//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UnikeyFactoryTest.Context
{
    using System;
    using System.Collections.Generic;
    
    public partial class AdministratedTest
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AdministratedTest()
        {
            this.AdministratedQuestions = new HashSet<AdministratedQuestion>();
        }
    
        public int Id { get; set; }
        public string URL { get; set; }
        public string Title { get; set; }
        public int MaxScore { get; set; }
        public Nullable<int> TestId { get; set; }
        public string TestSubject { get; set; }
        public System.DateTime Date { get; set; }
        public byte State { get; set; }
        public int Score { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AdministratedQuestion> AdministratedQuestions { get; set; }
        public virtual Test Test { get; set; }
    }
}
