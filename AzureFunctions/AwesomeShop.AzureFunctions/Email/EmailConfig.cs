using System;

namespace AwesomeShop.AzureFunctions.Email
{
    public class EmailConfig
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string Sender { get; set; }
        public string Password { get; set; }

        public EmailConfig(string host, int port, string sender, string password)
        {
            Host = host ?? throw new ArgumentNullException(nameof(host));
            Port = port;
            Sender = sender ?? throw new ArgumentNullException(nameof(sender));
            Password = password ?? throw new ArgumentNullException(nameof(password));
        }
    }
}