using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using SB.Shared.EntityProviders;
using SB.Shop.AzureFunctionApp.Helpers;
using System;

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
        public void Run([TimerTrigger("0 0 9 * * *")] TimerInfo myTimer, ILogger log)
        {
            var contacts= new Contact(_organizationServiceConfigurator.organizationService).GetContactsBirthdayToday();

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