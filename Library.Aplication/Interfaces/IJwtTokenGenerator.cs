using Library.Domain.Entities;

namespace Library.Aplication.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
    }
}
