using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SampleApp.Core.Interface;
using SampleApp.Core.Services;
using SampleApp.Infrastructure;
using SampleApp.Cli.Controllers;
using SampleApp.Cli.Models;
using SampleApp.Cli.Presenters;
using SampleApp.Cli.Views;

namespace SampleApp.Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                // .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            var serviceProvider = new ServiceCollection()
                .AddLogging(loggingBuilder =>
                {
                    loggingBuilder.AddConsole();
                })
                // リポジトリ
                //.AddScoped<IUserRepository, StubUserRepository>()
                .AddScoped<IUserRepository, CsvUserRepository>()
                .Configure<CsvUserRepositoryOptions>(options =>
                {
                    options.FileName = "./Data/User.csv";
                })
                // ビュー
                .AddTransient<SampleAppView>()
                // コントローラ
                .AddTransient<HelloController>()
                // ユースケース
                .AddTransient<IHello, Hello>()
                .AddTransient<IHelloPresenter, HelloPresenter>()
                .AddScoped<HelloViewModel>()
                .BuildServiceProvider();

            while (true)
            {
                using (var scope = serviceProvider.CreateScope())
                {
                    var helloController = scope.ServiceProvider.GetService<HelloController>();
                    var view = scope.ServiceProvider.GetService<SampleAppView>();

                    view.UserIdInput += (sender, userId) =>
                    {
                        helloController.Hello(userId).Wait();
                    };

                    view.Prompt();
                }
            }
        }
    }
}
