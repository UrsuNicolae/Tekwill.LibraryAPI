using Library.Domain.Common;
using Library.Domain.Entities;

namespace Library.Aplication.Interfaces
{
    public interface ICategoryRepository
    {
        Task<PaginatedList<Category>> GetCategorys(int page, int pageSize, CancellationToken ct = default);
        Task<Category?> GetCategoryById(int id, CancellationToken ct = default);

        Task CreateCategory(Category book, CancellationToken ct = default);
        Task UpdateCategory(Category book, CancellationToken ct = default);
        Task DeleteCategory(int id, CancellationToken ct = default);
    }
}
