using Library.Aplication.DTOs.Authors;

namespace Library.Aplication.DTOs.Books
{
    public class BookDto : CreateBookDto
    {
        public int Id { get; set; }

        public AuthorDto? Author { get; set; }

        public override string ToString()
        {
            return $"BookId: {Id} \n" +
                $"BookTitle: {Title}\n" +
                $"ISBN: {ISBN}";
        }
    }

}
