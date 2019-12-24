using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MasterDetail.DAL;
using MasterDetail.Domain;
using MasterDetail.Domain.IRepository;

namespace MasterDetail.Business
{
    
    public class AuthorService
    {
        private  IAuthorRepository Repo { get; set; }

        public AuthorService()
        {
            Repo = new AuthorRepository();
        }

        public void AddNewAuthor(Author author)
        {
            Repo.Create(author);
        }
        public Author GetAuthor(int Id)
        {
            return Repo.Read(Id);
        }

        public List<Author> GetAllAuthors()
        {
            return Repo.GetAll();
        }

        public List<Book> GetAllBooksOfAuthor(int authId)
        {
            return Repo.GetAllBooksOfAuthor(authId);
        }
    }
}
