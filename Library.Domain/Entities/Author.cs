namespace Library.Domain.Entities
{
    public class Author
    {
        public Author()
        {
            
        }
        public Author(Author author)
        {
            Id = author.Id;
            FirstName = author.FirstName;
            LastName = author.LastName;
            BirthDate = author.BirthDate;
            Site = author.Site;
        }
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public string Site { get; set; }

        public ICollection<AuthorGenres>? AuthorGenres { get; set; }

        public ICollection<Book>? Books { get; set; }
    }
}
