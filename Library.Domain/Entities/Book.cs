namespace Library.Domain.Entities
{
    public class Book
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int CategoryId { get; set; }

        public string ISBN { get; set; }

        public int Tiraj { get; set; }

        public Category? Category { get; set; }

        public int AuthorId { get; set; }

        public Author? Author { get; set; }
    }
}
