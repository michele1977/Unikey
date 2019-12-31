using System;
using System.Collections.Generic;
using System.Linq;

namespace ClassLibrary1.Business.Entities
{
    public class ExQuestion : IEquatable<ExQuestion>
    {
        #region IEquatable
        public bool Equals(ExQuestion other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            var returned = Id == other.Id
                           && QuestionText == other.QuestionText
                           && Type == other.Type
                           && Position == other.Position
                           && QuestionId == other.QuestionId;
                   
            return ExPossibleAnswers.Aggregate(returned, (current, answer) => current && other.ExPossibleAnswers.Contains(answer));

        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((ExQuestion) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Id;
                hashCode = (hashCode * 397) ^ (QuestionText != null ? QuestionText.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Type.GetHashCode();
                hashCode = (hashCode * 397) ^ Position;
                hashCode = (hashCode * 397) ^ (QuestionId.HasValue ? QuestionId.Value : 0);
                hashCode = (hashCode * 397) ^ (ExPossibleAnswers != null ? ExPossibleAnswers.GetHashCode() : 0);
                return hashCode;
            }
        }

        public static bool operator ==(ExQuestion left, ExQuestion right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ExQuestion left, ExQuestion right)
        {
            return !Equals(left, right);
        }
        #endregion

        #region Properties

        public int Id { get; set; }
        public int? QuestionId { get; set; }
        public string QuestionText { get; set; }
        public byte Type { get; set; }
        public int Position { get; set; }
        public List<ExPossibleAnswer> ExPossibleAnswers { get; set; }
        #endregion
    }
}
