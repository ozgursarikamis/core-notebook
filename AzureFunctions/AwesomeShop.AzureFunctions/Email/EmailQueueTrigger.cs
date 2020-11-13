using System;
using System.Threading.Tasks;
using AwesomeShop.AzureFunctions.Infrastructure;
using AwesomeShop.QueueLibrary.Infrastructure;
using AwesomeShop.QueueLibrary.Messages;
using AwesomeShop.QueueLibrary.QueueConnection;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace AwesomeShop.AzureFunctions.Email
{
    public static class EmailQueueTrigger
    {
        [FunctionName("EmailQueueTrigger")]
        public static async Task Run(
            [QueueTrigger(RouteNames.EmailBox, Connection = "AzureWebJobsStorage")]string message, 
            ILogger log
            )
        {
            log.LogInformation($"C# Queue trigger function processed: {message}");
            try
            {
                var communicator = DIContainer.Instance.GetService<IQueueCommunicator>();
                var command = communicator.Read<SendEmailCommand>(message);

                var handler = DIContainer.Instance.GetService<ISendEmailCommandHandler>();
                await handler.Handle(command);
            }
            catch (Exception e)
            {
                log.LogError(e, $"something went wrong with the EmailQueueTrigger");
                throw; // to requeue, do not burie your exceptions
            }
        }
    }
}
