using Library.Aplication.Interfaces;
using Library.Infrastructure.Persistance;
using Quartz;
using Telegram.Bot;

namespace LibraryBot
{
    [DisallowConcurrentExecution]
    public class LibraryNotificationJob : IJob
    {
        private readonly ILogger<LibraryNotificationJob> _logger;
        private readonly ITelegramBotClient _bot;
        private readonly IServiceScopeFactory _scopeFactory;

        public LibraryNotificationJob(
            ILogger<LibraryNotificationJob> logger,
            ITelegramBotClient bot,
            IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            _bot = bot;
            _scopeFactory = scopeFactory;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation($"{nameof(LibraryNotificationJob)} has started!");
            try
            {
                using var scope = _scopeFactory.CreateAsyncScope();
                var chatRepo = scope.ServiceProvider.GetRequiredService<IChatRepository>();
                var bookRepository = scope.ServiceProvider.GetRequiredService<IBookRepository>();
                var chats = await chatRepo.GetAllChatsForNewBookNotification();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            finally
            {
                _logger.LogInformation($"{nameof(LibraryNotificationJob)} has ended!");
            }
        }
    }
}
