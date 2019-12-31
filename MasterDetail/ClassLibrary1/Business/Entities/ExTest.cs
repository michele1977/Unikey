using System;
using System.Collections.Generic;
using System.Linq;

namespace ClassLibrary1.Business.Entities
{
    public class ExTest : IEquatable<ExTest>
    {
        #region IEquatable
        public bool Equals(ExTest other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            var returned =  Id == other.Id 
                   && TestId == other.TestId 
                   && ExecutionDate.Equals(other.ExecutionDate) 
                   && Name == other.Name 
                   && State == other.State ;
            return ExQuestions.Aggregate(returned, (current, question) => current && other.ExQuestions.Contains(question));

        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((ExTest) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Id;
                hashCode = (hashCode * 397) ^ (TestId.HasValue ? TestId.Value : 0);
                hashCode = (hashCode * 397) ^ ExecutionDate.GetHashCode();
                hashCode = (hashCode * 397) ^ (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ State.GetHashCode();
                hashCode = (hashCode * 397) ^ (ExQuestions != null ? ExQuestions.GetHashCode() : 0);
                return hashCode;
            }
        }

        public static bool operator ==(ExTest left, ExTest right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ExTest left, ExTest right)
        {
            return !Equals(left, right);
        }
        #endregion

        #region Properties
        public int Id { get; set; }
        public int? TestId { get; set; }
        public DateTime ExecutionDate { get; set; }
        public string Name { get; set; }
        public byte State { get; set; }
        public List<ExQuestion> ExQuestions { get; set; }
        #endregion

    }
}
