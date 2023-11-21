using Microsoft.Xrm.Sdk;

namespace SB.Shop.AzureFunctionApp.Helpers
{
    public interface IOrganizationServiceConfigurator
    {
        IOrganizationService GetOrganizationService();
        void Execute(OrganizationRequest reques);
    }
}