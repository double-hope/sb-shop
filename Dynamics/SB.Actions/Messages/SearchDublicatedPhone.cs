using Microsoft.Xrm.Sdk;
using SB.Shared;
using SB.Shared.EntityProviders;
using SB.Shared.Models.Actions;

namespace SB.Actions.Messages
{
    public class SearchDublicatedPhone : IActionTracking
    {
        private readonly IOrganizationService _organizationService;

        public SearchDublicatedPhone(IOrganizationService organizationService)
        {
            _organizationService = organizationService;
        }

        public void Execute(string parameters, ref ActionResponse actionResponse)
        {
            var deseriazisedParams = JsonSerializer.Deserialize<SearchDublicatedPhoneParams>(parameters);

            var user = new Contact(_organizationService).GetDublicatedPhoneUser(deseriazisedParams.PhoneNumber);

            var res = new SearchDublicatedPhoneResponse
            {
                DublicatedUserId = user?.Id,
            };

            actionResponse.Value = JsonSerializer.Serialize(res);
        }
    }
}