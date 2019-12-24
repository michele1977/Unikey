using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterDetail.Domain
{
    public class Author
    {
        #region Equatable
        public bool Equals(Author other)
        {
            //if (ReferenceEquals(null, other)) return false;
            //if (ReferenceEquals(this, other)) return true;
            return Name == other.Name 
                   && LastName == other.LastName 
                   && Equals(Books, other.Books);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Author) obj);
        }

        public override int GetHashCode()
        {
            var hashCode = (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (LastName != null ? LastName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Books != null ? Books.GetHashCode() : 0);
                return hashCode;
        }

        public static bool operator ==(Author left, Author right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Author left, Author right)
        {
            return !Equals(left, right);
        }
        #endregion
       
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }

        public List<Book> Books { get; set; }
    }
}
