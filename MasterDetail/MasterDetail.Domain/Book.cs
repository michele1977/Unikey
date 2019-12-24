using System;

namespace MasterDetail.Domain
{
    public class Book : IEquatable<Book>
    {
        #region Equatable
        public bool Equals(Book other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id && Title == other.Title && Year == other.Year;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Book)obj);
        }

        public override int GetHashCode()
        {
                var hashCode = Id;
                hashCode = (hashCode * 397) ^ (Title != null ? Title.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Year;
                return hashCode;
        }

        public static bool operator ==(Book left, Book right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Book left, Book right)
        {
            return !Equals(left, right);
        }
        #endregion 
    
        public int Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
    }
}