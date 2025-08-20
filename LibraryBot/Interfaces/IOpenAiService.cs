using Library.Aplication.DTOs.Books;

namespace LibraryBot.Interfaces
{
    public interface IOpenAiService
    {
        Task<string> GetBookRecommandation(BookDto book, int count = 3, CancellationToken ct = default);
    }
}
