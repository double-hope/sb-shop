using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.Xrm.Sdk;
using SB.Shared;
using SB.Shared.Models.Actions;
using SB.Shop.AzureFunctionApp.Helpers;
using System;

namespace SB.Shop.AzureFunctionApp.Functions
{
    public class ServiceBusTrigger
    {
        private IOrganizationServiceConfigurator _organizationServiceConfigurator;

        public ServiceBusTrigger(IOrganizationServiceConfigurator organizationServiceConfigurator)
        {
            _organizationServiceConfigurator = organizationServiceConfigurator;
        }

        [FunctionName("ServiceBusTrigger")]
        public void Run([ServiceBusTrigger("%QueueName%", Connection = "AzureWebJobsServiceBus")] string myQueueItem, ILogger log)
        {
            var actionName = Environment.GetEnvironmentVariable("ActionName");

            var Parameters = new SendBirthdayEmailParams()
            {
                ContactId = myQueueItem,
            };

            var underscoreIndex = actionName.IndexOf("_") + 1;

            _organizationServiceConfigurator
                .Execute(
                new OrganizationRequest(actionName)
                {
                    Parameters =
                    {
                        { "ActionName", actionName.Substring(underscoreIndex) },
                        { "Parameters", JsonSerializer.Serialize(Parameters) },
                    }
                });
        }
    }
}