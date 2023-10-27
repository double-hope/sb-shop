using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using System.Net;
using System;
using Polly;

namespace SB.Shop.AzureFunctionApp.Helpers
{
    public class OrganizationServiceConfigurator : IOrganizationServiceConfigurator
    {
        public IOrganizationService Configure()
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
    }
}
