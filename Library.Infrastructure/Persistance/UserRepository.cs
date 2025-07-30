using Library.Aplication.Interfaces;
using Library.Domain.Entities;
using Library.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Persistance
{
    public class UserRepository : IUserRepository
    {
        private readonly LibraryContext _context;
        public UserRepository(LibraryContext context)
        {
            _context = context;
        }
        public async Task CreateUser(User user, CancellationToken ct = default)
        {
            await _context.Users.AddAsync(user, ct);
            await _context.SaveChangesAsync(ct);
        }

        public async Task<User?> GetByEmail(string email, CancellationToken ct = default)
        {
            return await _context.Users.FirstOrDefaultAsync(e => e.Email == email, ct);
        }
    }
}
