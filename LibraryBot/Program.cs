using Telegram.Bot;
using Library.Infrastructure.Extensions;

namespace LibraryBot
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);
            builder.Services.AddHostedService<Worker>();
            builder.Services.ConfigureRepositories();
            builder.Services.ConfigureEfCore(builder.Configuration);
            builder.Services.AddSingleton<ITelegramBotClient>(sp =>
            {
                var token = builder.Configuration["bot_api_key"];
                return new TelegramBotClient(token);
            });

            var host = builder.Build();
            host.Run();
        }
    }
}