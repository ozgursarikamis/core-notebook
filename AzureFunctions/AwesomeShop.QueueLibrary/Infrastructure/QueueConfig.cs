namespace AwesomeShop.QueueLibrary.Infrastructure
{
    public class QueueConfig
    {
        public string QueueConnectionString { get; set; }

        public QueueConfig()
        {
            
        }

        public QueueConfig(string connectionString)
        {
            QueueConnectionString = connectionString;
        }
    }
}
