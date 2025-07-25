using Library.Aplication.Interfaces;
using Library.Domain.Common;
using Library.Domain.Entities;
using Library.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Persistance
{
    public class GenreRepository : IGenreRepository
    {
        private readonly LibraryContext _libraryContext;

        public GenreRepository(LibraryContext libraryContext)
        {
            _libraryContext = libraryContext;
        }
        public async Task CreateGen(Gen gen, CancellationToken ct = default)
        {
            await _libraryContext.Genres.AddAsync(gen, ct);
            await _libraryContext.SaveChangesAsync(ct);
        }

        public async Task DeleteGen(int id, CancellationToken ct = default)
        {
            var genToRemove = await _libraryContext.Genres.FirstOrDefaultAsync(g => g.Id == id, ct);
            if(genToRemove == null)
            {
                throw new KeyNotFoundException($"Genre with id: {id} not found!");
            }
            _libraryContext.Genres.Remove(genToRemove);
            await _libraryContext.SaveChangesAsync(ct);
        }

        public async Task<Gen?> GetGenById(int id, CancellationToken ct = default)
        {
            return await _libraryContext.Genres.FirstOrDefaultAsync(g => g.Id == id, ct);
        }

        public async Task<PaginatedList<Gen>> GetGens(int page, int pageSize, CancellationToken ct = default)
        {
            var total = await _libraryContext.Genres.CountAsync(ct);
            var genres = await _libraryContext.Genres
                .AsNoTracking()
                .OrderBy(a => a.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(ct);

            return new PaginatedList<Gen>(genres, page, (int)Math.Ceiling((double)total / pageSize));
        }

        public async Task UpdateGen(Gen gen, CancellationToken ct = default)
        {
            _libraryContext.Genres.Update(gen);
            await _libraryContext.SaveChangesAsync(ct);
        }
    }
}
