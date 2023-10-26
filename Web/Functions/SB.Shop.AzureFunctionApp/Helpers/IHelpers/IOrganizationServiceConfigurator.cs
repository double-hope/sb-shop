using Microsoft.Xrm.Sdk;

namespace SB.Shop.AzureFunctionApp.Helpers
{
    public interface IOrganizationServiceConfigurator
    {
        IOrganizationService Configure();
    }
}
