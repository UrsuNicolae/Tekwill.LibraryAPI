﻿using Library.Aplication.Interfaces;
using Library.Domain.Entities;
using Library.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Persistance
{
    public class ChatRepository : IChatRepository
    {
        private readonly LibraryContext _context;

        public ChatRepository(LibraryContext context)
        {
            _context = context;
        }
        public async Task CreateChat(Chat chat, CancellationToken ct = default)
        {
            await _context.Chats.AddAsync(chat, ct);
            await _context.SaveChangesAsync(ct);
        }

        public async Task<Chat?> GetChat(long id, CancellationToken ct = default)
        {
            return await _context.Chats.FirstOrDefaultAsync(c => c.Id == id, ct);
        }
    }
}
