namespace Library.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int TotalBooks { get; set; }

        public ICollection<Book>? Books { get; set; }
    }
}
