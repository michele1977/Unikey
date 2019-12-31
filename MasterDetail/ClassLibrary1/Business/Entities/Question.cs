using System;
using System.Collections.Generic;
using System.Linq;

namespace ClassLibrary1.Business.Entities
{
    public  class Question : IEquatable<Question>
    {
        #region IEquatable

        public bool Equals(Question other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            var returned =  Id == other.Id 
                            && QuestionText == other.QuestionText 
                            && State == other.State 
                            && Type == other.Type;
            returned = returned && PossibleAnswers.Aggregate(returned, (current, answer) => current && other.PossibleAnswers.Contains(answer));
            return returned;

        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Question) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Id;
                hashCode = (hashCode * 397) ^ (QuestionText != null ? QuestionText.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ State.GetHashCode();
                hashCode = (hashCode * 397) ^ Type.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==(Question left, Question right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Question left, Question right)
        {
            return !Equals(left, right);
        }

        #endregion

        #region Properties

        public int Id { get; set; }
        public string QuestionText { get; set; }
        public byte State { get; set; }
        public byte Type { get; set; }
        public virtual ICollection<PossibleAnswer> PossibleAnswers { get; set; }

        #endregion
    }
}
