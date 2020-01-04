using System;

namespace ClassLibrary1.Business.Entities
{
    public  class PossibleAnswer : IEquatable<PossibleAnswer>
    {
        #region IEquatable

        public bool Equals(PossibleAnswer other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            var returned =  Id == other.Id 
                   && Text == other.Text 
                   && IsCorrect == other.IsCorrect;
            return returned;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((PossibleAnswer) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Id;
                hashCode = (hashCode * 397) ^ (Text != null ? Text.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ IsCorrect.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==(PossibleAnswer left, PossibleAnswer right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(PossibleAnswer left, PossibleAnswer right)
        {
            return !Equals(left, right);
        }

        #endregion


        #region Properties

        public int Id { get; set; }
        public string Text { get; set; }
        public byte IsCorrect { get; set; }

        #endregion

    }
}
