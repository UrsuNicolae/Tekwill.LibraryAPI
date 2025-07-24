using Library.Aplication.Interfaces;
using Library.Domain.Common;
using Library.Domain.Entities;
using Library.Infrastructure.Data;

namespace Library.Infrastructure.Persistance
{
    public class GenreRepository : IGenreRepository
    {
        private readonly LibraryContext _libraryContext;

        public GenreRepository(LibraryContext libraryContext)
        {
            _libraryContext = libraryContext;
        }
        public Task CreateGen(Gen book, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task DeleteGen(int id, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task<Gen> GetGenById(int id, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task<PaginatedList<Gen>> GetGens(int page, int pageSize, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task UpdateGen(Gen book, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }
    }
}
