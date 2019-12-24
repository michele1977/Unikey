using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using MasterDetail.Domain;

namespace MasterDetail.Models.DTO
{
    public class AuthorDTO
    {
        public AuthorDTO()
        {

        }

        public AuthorDTO(Author author)
        {
            Id = author.Id;
            Name = author.Name;
            LastName = author.LastName;
            //Books = new List<BookDTO>();
            //Books = author.Books.Select(x => new BookDTO(x)).ToList();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        //public List<BookDTO> Books { get; set; }
    
    }
}