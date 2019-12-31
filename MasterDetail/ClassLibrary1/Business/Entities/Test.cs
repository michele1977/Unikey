using System;
using System.Collections.Generic;
using System.Linq;

namespace ClassLibrary1.Business.Entities
{
    public class Test : IEquatable<Test>
    {
        public bool Equals(Test other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            var returned = Id == other.Id 
                   && Title == other.Title 
                   && CreationDate.Equals(other.CreationDate) 
                   && State == other.State;
            returned = returned && Questions.Aggregate(returned, (current, question) => current && other.Questions.Contains(question));
            return returned;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Test) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Id;
                hashCode = (hashCode * 397) ^ (Title != null ? Title.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ CreationDate.GetHashCode();
                hashCode = (hashCode * 397) ^ State.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==(Test left, Test right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Test left, Test right)
        {
            return !Equals(left, right);
        }

        #region Properties

        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime CreationDate { get; set; }
        public byte State { get; set; }

        public virtual ICollection<Question> Questions { get; set; }

        #endregion
    }
}
