namespace Library.Domain.Entities
{
    public class AuthorGenres
    {
        public AuthorGenres()
        {
            
        }

        public AuthorGenres(int authorId, int genId)
        {
            AuthorId = authorId;
            GenId = genId;
        }
        public int AuthorId { get; set; }

        public Author? Author { get; set; }

        public int GenId { get; set; }

        public Gen? Gen { get; set; }
    }
}
