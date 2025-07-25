using Library.Domain.Common;
using Library.Domain.Entities;

namespace Library.Aplication.Interfaces
{
    public interface IAuthorRepository
    {
        Task<PaginatedList<Author>> GetAuthors(int page, int pageSize, CancellationToken ct = default);
        Task<Author?> GetAuthorById(int id, CancellationToken ct = default);

        Task CreateAuthor(Author book, CancellationToken ct = default);
        Task UpdateAuthor(Author book, CancellationToken ct = default);
        Task DeleteAuthor(int id, CancellationToken ct = default);
    }
}
