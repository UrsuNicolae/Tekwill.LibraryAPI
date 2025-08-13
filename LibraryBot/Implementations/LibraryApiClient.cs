using Library.Aplication.DTOs.Books;
using Library.Domain.Common;
using LibraryBot.Interfaces;
using System.Net.Http.Json;

namespace LibraryBot.Implementations
{
    public class LibraryApiClient : ILibraryApiClient
    {
        private readonly HttpClient _client;
        public LibraryApiClient(IHttpClientFactory httpClientFactory) 
        {
            _client = httpClientFactory.CreateClient("LibraryApiClient");
        }

        public async Task<BookDto> GetBookById(int id, CancellationToken ct)
        {
            return await _client.GetFromJsonAsync<BookDto>($"/Books/{id}", ct);
        }

        public Task<PaginatedList<BookDto>> GetPaginatedBooks(int pageSize, int pageIndex, CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}
