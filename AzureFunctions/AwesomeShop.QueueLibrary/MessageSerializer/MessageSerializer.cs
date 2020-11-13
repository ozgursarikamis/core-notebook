using Newtonsoft.Json;

namespace AwesomeShop.QueueLibrary.MessageSerializer
{
    public interface IMessageSerializer
    {
        T Deserializer<T>(string message);
        string Serializer(object obj);
    }

    public class MessageSerializer : IMessageSerializer
    {
        public T Deserializer<T>(string message)
        {
            throw new System.NotImplementedException();
        }

        public string Serializer(object obj)
        {
            throw new System.NotImplementedException();
        }
    }

    public class JsonMessageSerializer: IMessageSerializer
    {
        public T Deserializer<T>(string message)
        {
            var obj = JsonConvert.DeserializeObject<T>(message);
            return obj;
        }

        public string Serializer(object obj)
        {
            var message = JsonConvert.SerializeObject(obj);
            return message;
        }
    }
}
