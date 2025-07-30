using Library.Domain.Entities;

namespace Library.Aplication.Interfaces
{
    public interface IUserRepository
    {
        public Task<User?> GetByEmail(string email, CancellationToken ct = default);

        public Task CreateUser(User user, CancellationToken ct = default);
    }
}
