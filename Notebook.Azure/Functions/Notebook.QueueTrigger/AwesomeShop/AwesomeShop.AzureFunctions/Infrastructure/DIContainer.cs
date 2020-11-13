using System;
using System.IO;
using AwesomeShop.AzureFunctions.Email;
using AwesomeShop.QueueLibrary.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AwesomeShop.AzureFunctions.Infrastructure
{
    public static class DIContainer
    {
        public static IServiceProvider Instance { get; } = Build();

        static DIContainer(){}

        private static IServiceProvider Build()
        {
            var services = new ServiceCollection();

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            services.AddSingleton(new EmailConfig
            (
                configuration["EmailHost"], Convert.ToInt32(configuration["EmailPort"]),
                configuration["EmailSender"], configuration["EmailPassword"]
            ));

            services.AddSingleton<ISendEmailCommandHandler, SendEmailCommandHandler>();

            services.AddAzureQueueLibrary(configuration["AzureWebJobsStorage"]);

            return services.BuildServiceProvider();
        }
    }
}
