using MasterDetail.Domain;

namespace MasterDetail.Models.DTO
{
    public class BookDTO 
    {
        public BookDTO(Book book)
        {
            Id = book.Id;
            Title = book.Title;
            Year = book.Year;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
    }
}