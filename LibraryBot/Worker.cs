using Library.Aplication.Interfaces;
using Library.Domain.Entities;
using LibraryBot.Implementations;
using LibraryBot.Interfaces;
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

            using var scope = _scopeFactory.CreateAsyncScope();
            switch (text)
            {
                case string s when s.Equals("/start"):
                    {
                        var chatRepository = scope.ServiceProvider.GetRequiredService<IChatRepository>();
                        var chatFromDb = await chatRepository.GetChat(chatId, ct);
                        if (chatFromDb == null)
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
                            text: AvailableCommands(),
                            cancellationToken: ct);
                        break;
                    }
                case string s when s.StartsWith("/book:"):
                    {
                        var libraryApiClient = scope.ServiceProvider.GetRequiredService<ILibraryApiClient>();
                        var splits = s.Split(":", StringSplitOptions.RemoveEmptyEntries);
                        if (splits.Count() == 2 &&
                            int.TryParse(splits[1], out var id))
                        {
                            var book = await libraryApiClient.GetBookById(id, ct);
                            if (book == null)
                            {
                                await bot.SendMessage(
                                   chatId: chatId,
                                   text: "Upss book not found!",
                                   cancellationToken: ct);
                            }
                            else
                            {
                                await bot.SendMessage(
                                   chatId: chatId,
                                   text: book.ToString(),
                                   cancellationToken: ct);
                            }
                        }
                        else
                        {
                            await bot.SendMessage(
                                   chatId: chatId,
                                   text: "Invalid command",
                                   cancellationToken: ct);
                        }

                        break;
                    }
                case string s when s.StartsWith("/books:"):
                    {
                        var libraryApiClient = scope.ServiceProvider.GetRequiredService<ILibraryApiClient>();
                        var splits = s.Split(":", StringSplitOptions.RemoveEmptyEntries);
                        if (splits.Count() == 3 &&
                            int.TryParse(splits[1], out var pageSize)
                            && int.TryParse(splits[2], out var pageIndex))
                        {
                            var paginatedBooks = await libraryApiClient.GetPaginatedBooks(pageSize, pageIndex, ct);
                            if (paginatedBooks == null)
                            {
                                await bot.SendMessage(
                                   chatId: chatId,
                                   text: AvailableCommands(),
                                   cancellationToken: ct);
                            }
                            else
                            {
                                await bot.SendMessage(
                                   chatId: chatId,
                                   text: paginatedBooks.ToString(),
                                   cancellationToken: ct);
                            }
                        }
                        else
                        {
                            await bot.SendMessage(
                                   chatId: chatId,
                                   text: "Invalid command",
                                   cancellationToken: ct);
                        }

                        break;
                    }
                case string s when s.StartsWith("/author:"):
                    {
                        var libraryApiClient = scope.ServiceProvider.GetRequiredService<ILibraryApiClient>();
                        var splits = s.Split(":", StringSplitOptions.RemoveEmptyEntries);
                        if (splits.Count() == 2 &&
                            int.TryParse(splits[1], out var id))
                        {
                            var author = await libraryApiClient.GetAuthorById(id, ct);
                            if (author == null)
                            {
                                await bot.SendMessage(
                                   chatId: chatId,
                                   text: "Upss author not found!",
                                   cancellationToken: ct);
                            }
                            else
                            {
                                await bot.SendMessage(
                                   chatId: chatId,
                                   text: author.ToString(),
                                   cancellationToken: ct);
                            }
                        }
                        else
                        {
                            await bot.SendMessage(
                                   chatId: chatId,
                                   text: "Invalid command",
                                   cancellationToken: ct);
                        }

                        break;
                    }
                case string s when s.StartsWith("/authors:"):
                    {
                        var libraryApiClient = scope.ServiceProvider.GetRequiredService<ILibraryApiClient>();
                        var splits = s.Split(":", StringSplitOptions.RemoveEmptyEntries);
                        if (splits.Count() == 3 &&
                            int.TryParse(splits[1], out var pageSize)
                            && int.TryParse(splits[2], out var pageIndex))
                        {
                            var paginatedAuthors = await libraryApiClient.GetPaginatedAuthors(pageSize, pageIndex, ct);
                            if (paginatedAuthors == null)
                            {
                                await bot.SendMessage(
                                   chatId: chatId,
                                   text: AvailableCommands(),
                                   cancellationToken: ct);
                            }
                            else
                            {
                                await bot.SendMessage(
                                   chatId: chatId,
                                   text: paginatedAuthors.ToString(),
                                   cancellationToken: ct);
                            }
                        }
                        else
                        {
                            await bot.SendMessage(
                                   chatId: chatId,
                                   text: "Invalid command",
                                   cancellationToken: ct);
                        }

                        break;
                    }
                case string s when s.StartsWith("/categories:"):
                    {
                        var libraryApiClient = scope.ServiceProvider.GetRequiredService<ILibraryApiClient>();
                        var splits = s.Split(":", StringSplitOptions.RemoveEmptyEntries);
                        if (splits.Count() == 3 &&
                            int.TryParse(splits[1], out var pageSize)
                            && int.TryParse(splits[2], out var pageIndex))
                        {
                            var paginatedCategories = await libraryApiClient.GetPaginatedCategories(pageSize, pageIndex, ct);
                            if (paginatedCategories == null)
                            {
                                await bot.SendMessage(
                                   chatId: chatId,
                                   text: AvailableCommands(),
                                   cancellationToken: ct);
                            }
                            else
                            {
                                await bot.SendMessage(
                                   chatId: chatId,
                                   text: paginatedCategories.ToString(),
                                   cancellationToken: ct);
                            }
                        }
                        else
                        {
                            await bot.SendMessage(
                                   chatId: chatId,
                                   text: "Invalid command",
                                   cancellationToken: ct);
                        }

                        break;
                    }
                default:
                    await bot.SendMessage(
                               chatId: chatId,
                               text: "Invalid command",
                               cancellationToken: ct);
                    await Task.Delay(TimeSpan.FromMinutes(1));
                    break;
            }
        }

        private Task HandleError(ITelegramBotClient bot, Exception ex, CancellationToken ct)
        {
            _logger.LogError(ex, "Telegram error");
            return Task.CompletedTask;
        }

        private string AvailableCommands()
        {
            return "/book:<id> - returns book by id \n" +
                "/books:<pageSize>:<pageIndex> returns paginated books \n" +
                "/author:<id> - returns author by id \n" +
                "/authors:<pageSize>:<pageIndex> returns paginated authors \n" +
                "/catories:<pageSize>:<pageIndex> returns paginated categories";
        }
    }
}
