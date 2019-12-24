using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MasterDetail.Domain;
using MasterDetail.Models.DTO;

namespace MasterDetail.Models
{
    public class AuthIndexModel
    {
        public AuthIndexModel()
        {
            Authors = new List<AuthorDTO>();
            SelectedBooks = new List<BookDTO>();

        }
        public List<AuthorDTO> Authors { get; set; }
        public List<BookDTO> SelectedBooks { get; set; }
    }
}