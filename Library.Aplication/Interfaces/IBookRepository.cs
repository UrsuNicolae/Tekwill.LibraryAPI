using Library.Domain.Common;
using Library.Domain.Entities;

namespace Library.Aplication.Interfaces
{
    public interface IBookRepository
    {
        Task<PaginatedList<Book>> GetBooks(int page, int pageSize, CancellationToken ct = default);
        Task<Book> GetBookById(int id, CancellationToken ct = default);

        Task CreateBook(Book book, CancellationToken ct = default);
        Task UpdateBook(Book book, CancellationToken ct = default);
        Task DeleteBook(int id, CancellationToken ct = default);
    }
}
