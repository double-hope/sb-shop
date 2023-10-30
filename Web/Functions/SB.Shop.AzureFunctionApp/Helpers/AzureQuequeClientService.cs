using Microsoft.Azure.ServiceBus;
using System;
using System.Text;

namespace SB.Shop.AzureFunctionApp.Helpers
{
    public class AzureQuequeClientService : IAzureQuequeClientService
    {
        private QueueClient _client;

        private QueueClient ConfigureClient()
        {
            if (_client == null)
            {
                var connectionString = Environment.GetEnvironmentVariable("AzureWebJobsServiceBus");
                var queueName = Environment.GetEnvironmentVariable("QueueName");

                _client = new QueueClient(connectionString, queueName);
            }

            return _client;
        }

        public void SendMessage(string messageBody)
        {
            var client = ConfigureClient();
            var message = new Message(Encoding.UTF8.GetBytes(messageBody));
            client.SendAsync(message).Wait();
        }

        public void CloseClient()
        {
            var client = ConfigureClient();
            client.CloseAsync().Wait();
        }
    }
}