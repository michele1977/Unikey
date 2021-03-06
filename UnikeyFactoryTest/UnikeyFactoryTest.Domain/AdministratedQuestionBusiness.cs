﻿using System.Collections.Generic;

namespace UnikeyFactoryTest.Domain
{
    public class AdministratedQuestionBusiness
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int AdministratedTestId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AdministratedAnswerBusiness> AdministratedAnswers { get; set; }
    }
}
