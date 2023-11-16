using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using System;
using System.Net;

namespace SB.Shop.AzureFunctionApp.Helpers
{
    public class OrganizationServiceConfigurator : IOrganizationServiceConfigurator
    {
        public IOrganizationService organizationService { get { return Configure(); } }
        private IOrganizationService Configure()
        {
            if (organizationService == null)
            {
                try
                {
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                    var connectionString = Environment.GetEnvironmentVariable("DynamicsUser");
                    
                    var client = new ServiceClient(connectionString)
                    {
                        MaxRetryCount = 3,
                        RetryPauseTime = TimeSpan.FromSeconds(5)
                    };

                    if (client.IsReady)
                    {
                        return client;
                    }
                    throw new Exception("Error");
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return organizationService;
        }

        public void Execute(OrganizationRequest request)
        {
            organizationService.Execute(request);
        }
    }
}