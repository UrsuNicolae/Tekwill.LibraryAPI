﻿using Library.Aplication.Interfaces;
using Library.Domain.Common;
using Library.Domain.Entities;
using Library.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace Library.Infrastructure.Persistance
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly LibraryContext _libraryContext;

        public AuthorRepository(LibraryContext libraryContext)
        {
            _libraryContext = libraryContext;
        }
        public async Task CreateAuthor(Author book, CancellationToken ct = default)
        {
            await _libraryContext.Authors.AddAsync(book, ct);
            await _libraryContext.SaveChangesAsync(ct);
        }

        public async Task DeleteAuthor(int id, CancellationToken ct = default)
        {
            var authorToRemove = await _libraryContext.Authors.FirstOrDefaultAsync(a => a.Id == id);
            if(authorToRemove == null)
            {
                throw new KeyNotFoundException();
            }
            _libraryContext.Authors.Remove(authorToRemove);
            await _libraryContext.SaveChangesAsync();
        }

        public Task<Author> GetAuthorById(int id, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task<PaginatedList<Author>> GetAuthors(int page, int pageSize, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAuthor(Author author, CancellationToken ct = default)
        {
            _libraryContext.Authors.Update(author);
            await _libraryContext.SaveChangesAsync(ct);
        }
    }
}
