using Library.Aplication.Interfaces;
using Library.Domain.Common;
using Library.Domain.Entities;
using Library.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Persistance
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryContext _libraryContext;
        private static readonly Func<LibraryContext, int, Task<Book?>> GetBookByIdCompiled = EF.CompileAsyncQuery((LibraryContext context, int id) => context.Books.FirstOrDefault(a => a.Id == id));


        public BookRepository(LibraryContext libraryContext)
        {
            _libraryContext = libraryContext;
        }
        public async Task CreateBook(Book book, CancellationToken ct = default)
        {
            await _libraryContext.Books.AddAsync(book, ct);
            await _libraryContext.SaveChangesAsync(ct);
        }

        public async Task DeleteBook(int id, CancellationToken ct = default)
        {
            var bookToDelete = await _libraryContext.Books.FirstOrDefaultAsync(b => b.Id == id, ct);
            if(bookToDelete == null)
            {
                throw new KeyNotFoundException($"Book with Id: {id} not found!");
            }

            _libraryContext.Books.Remove(bookToDelete);
            await _libraryContext.SaveChangesAsync(ct);
        }

        public async Task<Book?> GetBookById(int id, CancellationToken ct = default) =>
            await GetBookByIdCompiled(_libraryContext, id);

        public async Task<PaginatedList<Book>> GetBooks(int page, int pageSize, CancellationToken ct = default)
        {
            var total = await _libraryContext.Books.CountAsync(ct);
            var books = await _libraryContext.Books
                .AsNoTracking()
                .OrderBy(a => a.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(ct);

            return new PaginatedList<Book>(books, page, (int)Math.Ceiling((double)total / pageSize));
        }

        public async Task UpdateBook(Book book, CancellationToken ct = default)
        {
            _libraryContext.Books.Update(book);
            await _libraryContext.SaveChangesAsync(ct);
        }
    }
}
