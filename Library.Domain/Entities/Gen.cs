namespace Library.Domain.Entities
{
    public class Gen
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<AuthorGenres>? AuthorGenres { get; set; }
    }
}
