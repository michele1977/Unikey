using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MasterDetail.Domain;
using MasterDetail.Domain.IRepository;

namespace MasterDetail.DAL
{
    public class AuthorRepository : IAuthorRepository
    {
        static readonly List<Author> InternalDb = new List<Author>();

        static AuthorRepository()
        {
            InternalDb.Add(new Author()
            {
                Id = 1,
                LastName = "Marinelli", Name = "Mike",
                Books = new List<Book>()
                {
                    new Book() {Id = 1, Title = "My Titile", Year = 2018},
                    new Book() {Id = 2, Title = "My Titile2", Year = 2019}
                }
            });
            InternalDb.Add(new Author()
                {
                    Id = 2,
                    LastName = "Marinelli  2",
                    Name = "Mike 22",
                    Books = new List<Book>() { new Book() { Id = 3, Title = "My Titile 22", Year = 2018 }, new Book() { Id = 4, Title = "My Titile 33", Year = 2019 } }
                }
            );
        }

        public void Create(Author author)
        {
            author.Id = new Random().Next(1,5000);
            foreach (var book in author.Books) book.Id = new Random().Next(1, 5000);
            InternalDb.Add(author);
        }

        public Author Read(int authId)
        {
            return InternalDb.FirstOrDefault(x => x.Id == authId);
        }

        public void Update(Author author)
        {
            throw new NotImplementedException();
        }

        public void Delete(int authId)
        {
            throw new NotImplementedException();
        }

        public List<Author> GetAll()
        {
            return InternalDb;
        }

        public List<Book> GetAllBooksOfAuthor(int authId)
        {
            return InternalDb.First(x => x.Id == authId).Books;
        }
    }
}
