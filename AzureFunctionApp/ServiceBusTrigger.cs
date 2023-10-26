using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace SB.Shop.AzureFunctionApp
{
    public class ServiceBusTrigger
    {
        [FunctionName("ServiceBusTrigger")]
        public void Run([ServiceBusTrigger("%QueueName%", Connection = "AzureWebJobsServiceBus")]string myQueueItem, ExecutionContext context, ILogger log)
        {
            log.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");
        }
    }
}
