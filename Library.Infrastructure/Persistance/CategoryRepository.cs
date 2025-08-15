﻿using Library.Aplication.Interfaces;
using Library.Domain.Common;
using Library.Domain.Entities;
using Library.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Persistance
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly LibraryContext _libraryContext;

        public CategoryRepository(LibraryContext libraryContext)
        {
            _libraryContext = libraryContext;
        }
        public async Task CreateCategory(Category category, CancellationToken ct = default)
        {
            await _libraryContext.Categories.AddAsync(category, ct);
            await _libraryContext.SaveChangesAsync(ct);
        }

        public async Task DeleteCategory(int id, CancellationToken ct = default)
        {
            var categoryToDelete = await _libraryContext.Categories.FirstOrDefaultAsync(c => c.Id == id, ct);
            if(categoryToDelete == null)
            {
                throw new KeyNotFoundException($"Category with id: {id} not found!");
            }
            _libraryContext.Categories.Remove(categoryToDelete);
            await _libraryContext.SaveChangesAsync(ct);
        }

        public async Task<Category?> GetCategoryById(int id, CancellationToken ct = default)
        {
            return await _libraryContext.Categories
                .FirstOrDefaultAsync(c => c.Id == id, ct);
        }

        public async Task<PaginatedList<Category>> GetCategorys(int page, int pageSize, CancellationToken ct = default)
        {
            var total = await _libraryContext.Categories.CountAsync(ct);
            var categories = await _libraryContext.Categories
                .AsNoTracking()
                .OrderBy(a => a.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(ct);

            return new PaginatedList<Category>(categories, page, (int)Math.Ceiling((double)total / pageSize));
        }

        public async Task UpdateCategory(Category category, CancellationToken ct = default)
        {
            _libraryContext.Categories.Update(category);
            await _libraryContext.SaveChangesAsync(ct);
        }
    }
}
