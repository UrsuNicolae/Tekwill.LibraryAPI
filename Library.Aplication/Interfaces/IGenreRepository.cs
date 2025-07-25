using Library.Domain.Common;
using Library.Domain.Entities;

namespace Library.Aplication.Interfaces
{
    public interface IGenreRepository
    {
        Task<PaginatedList<Gen>> GetGens(int page, int pageSize, CancellationToken ct = default);
        Task<Gen?> GetGenById(int id, CancellationToken ct = default);

        Task CreateGen(Gen gen, CancellationToken ct = default);
        Task UpdateGen(Gen gen, CancellationToken ct = default);
        Task DeleteGen(int id, CancellationToken ct = default);
    }
}
