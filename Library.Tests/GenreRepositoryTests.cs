using Library.Aplication.Interfaces;
using Library.Domain.Entities;
using Library.Infrastructure.Data;
using Library.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Library.Tests
{
    public class GenreRepositoryTests
    {
        [Fact]
        public async Task CreateGenShouldSaveANewGenInDb()
        {
            var options = new DbContextOptionsBuilder<LibraryContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using var libContext = new LibraryContext(options);
            IGenreRepository repo = new GenreRepository(libContext);

            var genToAdd = new Gen
            {
                Name = "Test Genre"
            };

            await repo.CreateGen(genToAdd);

            Assert.True(genToAdd.Id > 0);
            var genInDb = await libContext.Set<Gen>().FindAsync(genToAdd.Id);
            Assert.NotNull(genInDb);
            Assert.Equal("Test Genre", genInDb.Name);
        }

        [Fact]
        public async Task GetGenByIdShouldReturnGenWithAuthorGenres()
        {
            var options = new DbContextOptionsBuilder<LibraryContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using var libContext = new LibraryContext(options);

            var gen = new Gen { Name = "Mystery" };
            await libContext.Set<Gen>().AddAsync(gen);

            var author = new Author
            {
                FirstName = "John",
                LastName = "Doe",
                BirthDate = DateTime.Now.AddYears(-40),
                Site = "johndoe.com"
            };
            await libContext.Authors.AddAsync(author);
            await libContext.SaveChangesAsync();

            await libContext.AuthorGenres.AddAsync(new AuthorGenres
            {
                AuthorId = author.Id,
                GenId = gen.Id
            });
            await libContext.SaveChangesAsync();

            IGenreRepository repo = new GenreRepository(libContext);

            var result = await repo.GetGenById(gen.Id);

            Assert.NotNull(result);
            Assert.Equal(gen.Id, result.Id);
            Assert.Equal("Mystery", result.Name);
            Assert.NotNull(result.AuthorGenres);
            Assert.Single(result.AuthorGenres);
            Assert.Equal(author.Id, result.AuthorGenres.First().AuthorId);
        }

        [Fact]
        public async Task GetGensShouldReturnPaginatedList()
        {
            var options = new DbContextOptionsBuilder<LibraryContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using var libContext = new LibraryContext(options);

            for (int i = 1; i <= 8; i++)
            {
                await libContext.Set<Gen>().AddAsync(new Gen
                {
                    Name = $"Genre {i}"
                });
            }
            await libContext.SaveChangesAsync();

            IGenreRepository repo = new GenreRepository(libContext);

            var page1 = await repo.GetGens(1, 3);
            var page2 = await repo.GetGens(2, 3);
            var page3 = await repo.GetGens(3, 3);

            Assert.Equal(3, page1.Items.Count);
            Assert.Equal(3, page2.Items.Count);
            Assert.Equal(2, page3.Items.Count);
            Assert.Equal(3, page1.TotalPages);
        }

        [Fact]
        public async Task UpdateGenShouldUpdateExistingGen()
        {
            var options = new DbContextOptionsBuilder<LibraryContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using var libContext = new LibraryContext(options);

            var gen = new Gen { Name = "Original Name" };
            await libContext.Set<Gen>().AddAsync(gen);
            await libContext.SaveChangesAsync();

            IGenreRepository repo = new GenreRepository(libContext);

            gen.Name = "Updated Name";

            await repo.UpdateGen(gen);

            var updatedGen = await libContext.Set<Gen>().FindAsync(gen.Id);
            Assert.NotNull(updatedGen);
            Assert.Equal("Updated Name", updatedGen.Name);
        }

        [Fact]
        public async Task DeleteGenShouldRemoveGenFromDb()
        {
            var options = new DbContextOptionsBuilder<LibraryContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using var libContext = new LibraryContext(options);

            var gen = new Gen { Name = "Genre to Delete" };
            await libContext.Set<Gen>().AddAsync(gen);
            await libContext.SaveChangesAsync();

            IGenreRepository repo = new GenreRepository(libContext);

            await repo.DeleteGen(gen.Id);

            var deletedGen = await libContext.Set<Gen>().FindAsync(gen.Id);
            Assert.Null(deletedGen);
        }

        [Fact]
        public async Task DeleteGenShouldThrowIfGenDoesNotExist()
        {
            var options = new DbContextOptionsBuilder<LibraryContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using var libContext = new LibraryContext(options);
            IGenreRepository repo = new GenreRepository(libContext);

            await Assert.ThrowsAsync<KeyNotFoundException>(() => repo.DeleteGen(999));
        }

    }
}