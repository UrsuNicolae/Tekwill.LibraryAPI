using Library.Aplication.DTOs.Authors;
using Library.Domain.Entities;

namespace Library.Aplication.DTOs.Books
{
    public class BookDto : CreateBookDto
    {
        public int Id { get; set; }

        public AuthorDto? Author { get; set; }
    }
}
