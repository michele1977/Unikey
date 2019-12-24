using System.Collections.Generic;

namespace MasterDetail.Domain.IRepository
{
    public interface IAuthorRepository
    {
        void Create(Author author);
        Author Read(int authId);
        void Update(Author author);
        void Delete(int authId);
        List<Author> GetAll();
        List<Book> GetAllBooksOfAuthor(int authId);
    }
}