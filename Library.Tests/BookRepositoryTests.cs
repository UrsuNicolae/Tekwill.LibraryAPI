using Library.Aplication.Interfaces;
using Library.Domain.Common;
using Library.Domain.Entities;
using Library.Infrastructure.Data;
using Library.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Library.Tests
{
    public class BookRepositoryTests
    {
        [Fact]
        public async Task CreateBookShouldSaveANewBookInDb()
        {
            var options = new DbContextOptionsBuilder<LibraryContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using var libContext = new LibraryContext(options);

            var category = new Category { Name = "Fiction" };
            var author = new Author
            {
                FirstName = "John",
                LastName = "Doe",
                BirthDate = DateTime.Now.AddYears(-40),
                Site = "johndoe.com"
            };

            await libContext.Categories.AddAsync(category);
            await libContext.Authors.AddAsync(author);
            await libContext.SaveChangesAsync();

            IBookRepository repo = new BookRepository(libContext);

            var bookToAdd = new Book
            {
                Title = "Test Book",
                ISBN = "1234567890",
                Tiraj = 1000,
                CategoryId = category.Id,
                AuthorId = author.Id
            };

            await repo.CreateBook(bookToAdd);

            Assert.True(bookToAdd.Id > 0);
            var bookInDb = await libContext.Books.FindAsync(bookToAdd.Id);
            Assert.NotNull(bookInDb);
            Assert.Equal("Test Book", bookInDb.Title);
            Assert.Equal("1234567890", bookInDb.ISBN);
        }

        [Fact]
        public async Task GetBookByIdShouldReturnBookWithRelations()
        {
            var options = new DbContextOptionsBuilder<LibraryContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using var libContext = new LibraryContext(options);

            var category = new Category { Name = "Fiction" };
            var author = new Author
            {
                FirstName = "John",
                LastName = "Doe",
                BirthDate = DateTime.Now.AddYears(-40),
                Site = "johndoe.com"
            };

            await libContext.Categories.AddAsync(category);
            await libContext.Authors.AddAsync(author);

            var book = new Book
            {
                Title = "Test Book",
                ISBN = "1234567890",
                Tiraj = 1000,
                CategoryId = category.Id,
                AuthorId = author.Id
            };

            await libContext.Books.AddAsync(book);
            await libContext.SaveChangesAsync();

            IBookRepository repo = new BookRepository(libContext);

            var result = await repo.GetBookById(book.Id);

            Assert.NotNull(result);
            Assert.Equal(book.Id, result.Id);
            Assert.Equal("Test Book", result.Title);
            Assert.NotNull(result.Author);
            Assert.Equal("John", result.Author.FirstName);
            Assert.NotNull(result.Category);
            Assert.Equal("Fiction", result.Category.Name);
        }

        [Fact]
        public async Task GetBooksShouldReturnPaginatedList()
        {
            var options = new DbContextOptionsBuilder<LibraryContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using var libContext = new LibraryContext(options);

            var category = new Category { Name = "Fiction" };
            var author = new Author
            {
                FirstName = "John",
                LastName = "Doe",
                BirthDate = DateTime.Now.AddYears(-40),
                Site = "johndoe.com"
            };

            await libContext.Categories.AddAsync(category);
            await libContext.Authors.AddAsync(author);
            await libContext.SaveChangesAsync();

            for (int i = 1; i <= 12; i++)
            {
                await libContext.Books.AddAsync(new Book
                {
                    Title = $"Book {i}",
                    ISBN = $"ISBN-{i}",
                    Tiraj = 1000 + i,
                    CategoryId = category.Id,
                    AuthorId = author.Id
                });
            }
            await libContext.SaveChangesAsync();

            IBookRepository repo = new BookRepository(libContext);

            var page1 = await repo.GetBooks(1, 5);
            var page2 = await repo.GetBooks(2, 5);
            var page3 = await repo.GetBooks(3, 5);

            Assert.Equal(5, page1.Items.Count);
            Assert.Equal(5, page2.Items.Count);
            Assert.Equal(2, page3.Items.Count);
            Assert.Equal(3, page1.TotalPages);
        }

        [Fact]
        public async Task UpdateBookShouldUpdateExistingBook()
        {
            var options = new DbContextOptionsBuilder<LibraryContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using var libContext = new LibraryContext(options);

            var category = new Category { Name = "Fiction" };
            var author = new Author
            {
                FirstName = "John",
                LastName = "Doe",
                BirthDate = DateTime.Now.AddYears(-40),
                Site = "johndoe.com"
            };

            await libContext.Categories.AddAsync(category);
            await libContext.Authors.AddAsync(author);

            var book = new Book
            {
                Title = "Original Title",
                ISBN = "1234567890",
                Tiraj = 1000,
                CategoryId = category.Id,
                AuthorId = author.Id
            };

            await libContext.Books.AddAsync(book);
            await libContext.SaveChangesAsync();

            IBookRepository repo = new BookRepository(libContext);

            book.Title = "Updated Title";
            book.ISBN = "0987654321";

            await repo.UpdateBook(book);

            var updatedBook = await libContext.Books.FindAsync(book.Id);
            Assert.NotNull(updatedBook);
            Assert.Equal("Updated Title", updatedBook.Title);
            Assert.Equal("0987654321", updatedBook.ISBN);
        }

        [Fact]
        public async Task DeleteBookShouldRemoveBookFromDb()
        {
            var options = new DbContextOptionsBuilder<LibraryContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using var libContext = new LibraryContext(options);

            var category = new Category { Name = "Fiction" };
            var author = new Author
            {
                FirstName = "John",
                LastName = "Doe",
                BirthDate = DateTime.Now.AddYears(-40),
                Site = "johndoe.com"
            };

            await libContext.Categories.AddAsync(category);
            await libContext.Authors.AddAsync(author);

            var book = new Book
            {
                Title = "Book to Delete",
                ISBN = "1234567890",
                Tiraj = 1000,
                CategoryId = category.Id,
                AuthorId = author.Id
            };

            await libContext.Books.AddAsync(book);
            await libContext.SaveChangesAsync();

            IBookRepository repo = new BookRepository(libContext);

            await repo.DeleteBook(book.Id);

            var deletedBook = await libContext.Books.FindAsync(book.Id);
            Assert.Null(deletedBook);
        }

        [Fact]
        public async Task DeleteBookShouldThrowIfBookDoesNotExist()
        {
            var options = new DbContextOptionsBuilder<LibraryContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using var libContext = new LibraryContext(options);
            IBookRepository repo = new BookRepository(libContext);

            await Assert.ThrowsAsync<KeyNotFoundException>(() => repo.DeleteBook(999));
        }

    }
}