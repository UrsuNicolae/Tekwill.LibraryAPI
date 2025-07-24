using Library.Aplication.Interfaces;
using Library.Domain.Common;
using Library.Domain.Entities;
using Library.Infrastructure.Data;

namespace Library.Infrastructure.Persistance
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly LibraryContext _libraryContext;

        public CategoryRepository(LibraryContext libraryContext)
        {
            _libraryContext = libraryContext;
        }
        public Task CreateCategory(Category book, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCategory(int id, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task<Category> GetCategoryById(int id, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task<PaginatedList<Category>> GetCategorys(int page, int pageSize, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task UpdateCategory(Category book, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }
    }
}
