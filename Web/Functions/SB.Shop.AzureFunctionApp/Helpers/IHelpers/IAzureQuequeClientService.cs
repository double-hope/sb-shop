using Microsoft.Azure.ServiceBus;

namespace SB.Shop.AzureFunctionApp.Helpers
{
    public interface IAzureQuequeClientService
    {
        void SendMessage(string messageBody);
        void CloseClient();
    }
}
