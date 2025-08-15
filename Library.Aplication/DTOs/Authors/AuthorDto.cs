namespace Library.Aplication.DTOs.Authors
{
    public class AuthorDto : CreateAuthorDto
    {
        public int Id { get; set; }

        public override string ToString()
        {
            var age = (int)Math.Ceiling((DateTime.UtcNow - BirthDate).TotalDays / 356);
            return $"Author Name: {FirstName} {LastName} \n " +
                $"Author Age: {age} \n" +
                $"Author Site: {Site} \n";
        }
    }
}
