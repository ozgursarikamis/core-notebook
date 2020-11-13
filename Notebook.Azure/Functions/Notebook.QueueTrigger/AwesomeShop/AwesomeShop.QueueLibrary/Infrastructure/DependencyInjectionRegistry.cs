using AwesomeShop.QueueLibrary.MessageSerializer;
using AwesomeShop.QueueLibrary.QueueConnection;
using Microsoft.Extensions.DependencyInjection;

namespace AwesomeShop.QueueLibrary.Infrastructure
{
    public static class DependencyInjectionRegistry
    {
        public static IServiceCollection AddAzureQueueLibrary(this IServiceCollection service, string connectionString)
        {
            service.AddSingleton(new QueueConfig(connectionString));
            service.AddSingleton<ICloudQueueClientFactory, CloudQueueClientFactory>();
            service.AddSingleton<IMessageSerializer, JsonMessageSerializer>();
            service.AddTransient<IQueueCommunicator, QueueCommunicator>();

            return service;
        }
    }
}
