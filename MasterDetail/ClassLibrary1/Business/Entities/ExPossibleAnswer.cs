using System;

namespace ClassLibrary1.Business.Entities
{
    public class ExPossibleAnswer : IEquatable<ExPossibleAnswer>
    {
        #region IEquatable
        public bool Equals(ExPossibleAnswer other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id 
                   && Text == other.Text 
                   && IsCorrect == other.IsCorrect 
                   && IsSelected == other.IsSelected;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((ExPossibleAnswer) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Id;
                hashCode = (hashCode * 397) ^ (Text != null ? Text.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ IsCorrect.GetHashCode();
                hashCode = (hashCode * 397) ^ IsSelected.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==(ExPossibleAnswer left, ExPossibleAnswer right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ExPossibleAnswer left, ExPossibleAnswer right)
        {
            return !Equals(left, right);
        }
        #endregion

        #region Properties
        public int Id { get; set; }
        public string Text { get; set; }
        public byte IsCorrect { get; set; }
        public bool IsSelected { get; set; }


        #endregion


    }
}
