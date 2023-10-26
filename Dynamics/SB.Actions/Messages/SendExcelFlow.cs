using Microsoft.Xrm.Sdk;
using SB.Shared;
using SB.Shared.Dynamics.EntityProviders;
using SB.Shared.EntityProviders;
using SB.Shared.Models.Actions;
using SB.Shared.Models.Dynamics;

namespace SB.Actions.Messages
{
    public class SendExcelFlow : IActionTracking
    {

        private readonly IOrganizationService _organizationService;
        private readonly IPluginExecutionContext _context;
        public SendExcelFlow(IOrganizationService organizationService, IPluginExecutionContext context)
        {
            _organizationService = organizationService;
            _context = context;

        }
        public void Execute(string parameters, ref ActionResponse actionResponse)
        {
            var deserializedParams = JsonSerializer.Deserialize<SendExcelFlowParams>(parameters);
            var viewProvider = new View(_organizationService);
            var view = viewProvider.GetEntityView(deserializedParams.EntityName, deserializedParams.ViewName);
            var user = new User(_organizationService, _context.UserId);

            var settings = new Settings(_organizationService).GetSettings(SBCustomSettingsModel.Fields.Powerautomatelink);

            user.SendFileToUser(view, settings);
        }
    }
}
