using AwesomeShop.QueueLibrary.Infrastructure;

namespace AwesomeShop.QueueLibrary.Messages
{
    public class BaseQueueMessage
    {
        public string Route { get; set; }

        public BaseQueueMessage(string route)
        {
            Route = route;
        }
    }

    public class SendEmailCommand : BaseQueueMessage
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        
        public SendEmailCommand() : base(RouteNames.EmailBox)
        {
        }
    }
}
