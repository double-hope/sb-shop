using System;
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
        private IAzureQuequeClientService _azureQuequeClientService;

        public TimerTrigger(IOrganizationServiceConfigurator organizationServiceConfigurator, IAzureQuequeClientService azureQuequeClientService)
        {
            _organizationServiceConfigurator = organizationServiceConfigurator;
            _azureQuequeClientService = azureQuequeClientService;
        }

        [FunctionName("TimerTrigger")]
        //public void Run([TimerTrigger("*/5 * * * * *")] TimerInfo myTimer, ILogger log)
        public void Run([TimerTrigger("0 0 9 * * *")] TimerInfo myTimer, ILogger log)
        {
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
                    _azureQuequeClientService.SendMessage(c.Id.ToString());
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _azureQuequeClientService.CloseClient();
            }
        }
    }
}
