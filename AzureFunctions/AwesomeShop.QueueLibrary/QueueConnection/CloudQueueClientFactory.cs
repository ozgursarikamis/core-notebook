using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AwesomeShop.QueueLibrary.Infrastructure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;

namespace AwesomeShop.QueueLibrary.QueueConnection
{
    public interface ICloudQueueClientFactory
    {
        CloudQueueClient GetClient();
    }

    public class CloudQueueClientFactory : ICloudQueueClientFactory
    {
        private QueueConfig Config { get; }
        private CloudQueueClient _cloudQueueClient;

        public CloudQueueClientFactory(QueueConfig config)
        {
            Config = config;
        }

        public CloudQueueClient GetClient()
        {
            if (_cloudQueueClient != null)
            {
                return _cloudQueueClient;
            }

            var storageAccount = CloudStorageAccount.Parse(Config.QueueConnectionString);
            _cloudQueueClient = storageAccount.CreateCloudQueueClient();
            return _cloudQueueClient;
        }
    }
}
