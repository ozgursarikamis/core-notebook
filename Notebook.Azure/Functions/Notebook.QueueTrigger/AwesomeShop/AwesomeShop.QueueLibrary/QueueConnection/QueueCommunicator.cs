using System.Threading.Tasks;
using AwesomeShop.QueueLibrary.Messages;
using AwesomeShop.QueueLibrary.MessageSerializer;
using Microsoft.WindowsAzure.Storage.Queue;

namespace AwesomeShop.QueueLibrary.QueueConnection
{
    public interface IQueueCommunicator
    {
        T Read<T>(string message);
        Task SendAsync<T>(T obj) where T : BaseQueueMessage;
    }

    public class QueueCommunicator : IQueueCommunicator
    {
        private readonly IMessageSerializer _serializer;
        private readonly ICloudQueueClientFactory _queueClientFactory;

        public QueueCommunicator(IMessageSerializer serializer, ICloudQueueClientFactory queueClientFactory)
        {
            _serializer = serializer;
            _queueClientFactory = queueClientFactory;
        }
        public T Read<T>(string message)
        {
            return _serializer.Deserializer<T>(message);
        }

        public async Task SendAsync<T>(T obj) where T : BaseQueueMessage
        {
            var queueReference = _queueClientFactory.GetClient().GetQueueReference(obj.Route);
            await queueReference.CreateIfNotExistsAsync();

            var serializedMessage = _serializer.Serializer(obj);
            var queueMessage = new CloudQueueMessage(serializedMessage);

            await queueReference.AddMessageAsync(queueMessage);
        }
    }
}