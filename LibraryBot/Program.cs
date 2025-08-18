using Telegram.Bot;
using Library.Infrastructure.Extensions;
using Library.Aplication.Interfaces;
using Library.Infrastructure.Configurations;
using Library.Infrastructure.ExternaServices;
using LibraryBot.Interfaces;
using LibraryBot.Implementations;
using Quartz;

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
            builder.Services.AddScoped<ILibraryApiClient, LibraryApiClient>();
            builder.Services.AddSingleton<ITelegramBotClient>(sp =>
            {
                var token = builder.Configuration["bot_api_key"];
                return new TelegramBotClient(token);
            });

            builder.Services.AddHttpClient(Constants.LibraryApiClient, (_, c) =>
            {
                c.BaseAddress = new Uri(builder.Configuration["LibraryApiConfig:BaseAddress"]);
                c.DefaultRequestHeaders.Accept.Add(
                    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                c.DefaultRequestHeaders.Add("x-app-name", builder.Configuration["LibraryApiConfig:AppName"]);
            });

            builder.Services.AddQuartz(q =>
            {
                var notificatJobKey = new JobKey(nameof(LibraryNotificationJob));
                q.AddJob<LibraryNotificationJob>(opts => opts.WithIdentity(notificatJobKey));
                q.AddTrigger(opts =>
                opts.ForJob(notificatJobKey)
                .WithIdentity($"{notificatJobKey}_trigger")
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(10)
                    .RepeatForever()
                    .WithMisfireHandlingInstructionNextWithExistingCount()));
            });

            builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

            var host = builder.Build();
            host.Run();
        }
    }
}