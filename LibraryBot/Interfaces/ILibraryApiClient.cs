using Library.Aplication.DTOs.Books;
using Library.Domain.Common;

namespace LibraryBot.Interfaces
{
    public interface ILibraryApiClient
    {
        Task<PaginatedList<BookDto>> GetPaginatedBooks(int pageSize, int pageIndex, CancellationToken ct);

        Task<BookDto> GetBookById(int id, CancellationToken ct);
    }
}
