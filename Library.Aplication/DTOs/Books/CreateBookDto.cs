using Library.Aplication.DTOs.Authors;
using Library.Aplication.DTOs.Categories;

namespace Library.Aplication.DTOs.Books
{
    public class CreateBookDto
    {

        public string Title { get; set; }

        public int CategoryId { get; set; }

        public string ISBN { get; set; }

        public int Tiraj { get; set; }

        public int AuthorId { get; set; }

        public CategoryDto? Category { get; set; }

    }
}
