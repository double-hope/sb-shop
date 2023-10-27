using System;
using System.Text;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.Xrm.Sdk.Query;
using SB.Shared.EntityProviders;
using SB.Shared.Models.Dynamics;
using SB.Shop.AzureFunctionApp.Helpers;

namespace SB.Shop.AzureFunctionApp.Functions
{
    public class TimerTrigger
    {
        private IOrganizationServiceConfigurator _organizationServiceConfigurator;

        public TimerTrigger(IOrganizationServiceConfigurator organizationServiceConfigurator)
        {
            _organizationServiceConfigurator = organizationServiceConfigurator;
        }

        [FunctionName("TimerTrigger")]
        //public void Run([TimerTrigger("*/5 * * * * *")] TimerInfo myTimer, ILogger log)
        public void Run([TimerTrigger("0 0 9 * * *")] TimerInfo myTimer, ILogger log)
        {
            var connectionString = Environment.GetEnvironmentVariable("AzureWebJobsServiceBus");
            var queueName = Environment.GetEnvironmentVariable("QueueName");

            var client = new QueueClient(connectionString, queueName);

            var date = DateTime.Now.ToString("dd.MM");
            var contactProvider = new Contact(_organizationServiceConfigurator.Configure());
            
            var filter = new FilterExpression
            {
                Conditions =
                {
                    new ConditionExpression
                    {
                        AttributeName = ContactModel.Fields.Birthdaythisyear,
                        Operator = ConditionOperator.Equal,
                        Values = { date }
                    }
                }
            };

            var contacts = contactProvider.GetContacts(filter, ContactModel.Fields.Birthdaythisyear);

            try
            {
                foreach (var c in contacts)
                {
                    var messageBody = c.Id.ToString();
                    var message = new Message(Encoding.UTF8.GetBytes(messageBody));
                    client.SendAsync(message).Wait();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                client.CloseAsync().Wait();
            }
        }
    }
}
