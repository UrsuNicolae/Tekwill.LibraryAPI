using Library.Domain.Entities;

namespace Library.Aplication.Interfaces
{
    public interface IChatRepository
    {
        Task CreateChat(Chat chat, CancellationToken ct = default);

        Task<Chat?> GetChat(long id, CancellationToken ct = default);
    }
}
