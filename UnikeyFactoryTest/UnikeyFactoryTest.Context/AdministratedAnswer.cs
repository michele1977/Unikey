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
    
    public partial class AdministratedAnswer
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public bool isCorrect { get; set; }
        public bool isSelected { get; set; }
        public int AdministratedQuestionId { get; set; }
        public decimal Score { get; set; }
    
        public virtual AdministratedQuestion AdministratedQuestion { get; set; }
    }
}
