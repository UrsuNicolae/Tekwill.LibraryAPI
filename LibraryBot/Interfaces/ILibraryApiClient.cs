using Library.Aplication.DTOs.Authors;
using Library.Aplication.DTOs.Books;
using Library.Aplication.DTOs.Categories;
using Library.Domain.Common;

namespace LibraryBot.Interfaces
{
    public interface ILibraryApiClient
    {
        Task<PaginatedList<BookDto>> GetPaginatedBooks(int pageSize, int pageIndex, CancellationToken ct);

        Task<BookDto?> GetBookById(int id, CancellationToken ct);

        Task<PaginatedList<AuthorDto>> GetPaginatedAuthors(int pageSize, int pageIndex, CancellationToken ct);
        Task<PaginatedList<CategoryDto>> GetPaginatedCategories(int pageSize, int pageIndex, CancellationToken ct);

        Task<AuthorDto?> GetAuthorById(int id, CancellationToken ct);
    }
}
