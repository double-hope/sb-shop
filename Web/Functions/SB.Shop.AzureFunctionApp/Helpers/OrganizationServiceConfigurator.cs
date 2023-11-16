using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Polly;
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

                    var retryPolicy = Policy
                        .Handle<Exception>()
                        .Retry(3);

                    var serviceClient = retryPolicy.Execute(() =>
                    {
                        var client = new ServiceClient(connectionString);
                        if (client.IsReady)
                        {
                            return client;
                        }
                        throw new Exception("Error");
                    });

                    return serviceClient;
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