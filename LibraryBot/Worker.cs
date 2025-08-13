using Library.Aplication.Interfaces;
using Library.Domain.Entities;
using SQLitePCL;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace LibraryBot
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly ITelegramBotClient _bot;
        private readonly IServiceScopeFactory _scopeFactory;

        public Worker(ILogger<Worker> logger, ITelegramBotClient bot,
            IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            _bot = bot;
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var receiver = new ReceiverOptions { AllowedUpdates = Array.Empty<UpdateType>() };
            _bot.StartReceiving(HandleUpdate, HandleError, receiver, stoppingToken);
        }

        private async Task HandleUpdate(ITelegramBotClient bot, Update update, CancellationToken ct)
        {
            var chatId = update.Message.Chat.Id;
            var text = update.Message.Text;
            if(text == "/start")
            {
                using var scope = _scopeFactory.CreateAsyncScope();
                var chatRepository = scope.ServiceProvider.GetRequiredService<IChatRepository>();

                var chatFromDb = await chatRepository.GetChat(chatId, ct);
                if(chatFromDb == null)
                {
                    var chatToCreate = new Library.Domain.Entities.Chat
                    {
                        Id = update.Message.Chat.Id,
                        UserName = update.Message.Chat.Username,
                        FirtName = update.Message.Chat.FirstName,
                        LastName = update.Message.Chat.LastName,
                        IsForm = update.Message.Chat.IsForum,
                        Type = update.Message.Chat.Type.ToString()
                    };
                    await chatRepository.CreateChat(chatToCreate, ct);
                }
                await bot.SendMessage(
                    chatId: chatId,
                    text: "LibraryBot has started!",
                    cancellationToken: ct);
            }
        }

        private Task HandleError(ITelegramBotClient bot, Exception ex, CancellationToken ct)
        {
            _logger.LogError(ex, "Telegram error");
            return Task.CompletedTask;
        }
    }
}
