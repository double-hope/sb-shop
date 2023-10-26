using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using System.Net;
using System;

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

                var serviceClient = new ServiceClient(connectionString);

                if (serviceClient != null)
                {
                    return serviceClient;
                }
                else
                {
                    throw new Exception("Error");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
