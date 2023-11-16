using Microsoft.Xrm.Sdk;

namespace SB.Shop.AzureFunctionApp.Helpers
{
    public interface IOrganizationServiceConfigurator
    {
        IOrganizationService organizationService { get; }
        void Execute(OrganizationRequest reques);
    }
}