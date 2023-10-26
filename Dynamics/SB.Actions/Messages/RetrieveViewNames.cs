using Microsoft.Xrm.Sdk;
using SB.Shared;
using SB.Shared.EntityProviders;
using SB.Shared.Models.Actions;
using System.Collections.Generic;

namespace SB.Actions.Messages
{
    public class RetrieveViewNames : IActionTracking
    {
        private readonly IOrganizationService _organizationService;
        private readonly IPluginExecutionContext _context;
        public RetrieveViewNames(IOrganizationService organizationService, IPluginExecutionContext context)
        {
            _organizationService = organizationService;
            _context = context;
        }
        public void Execute(string parameters, ref ActionResponse actionResponse)
        {
            var deserializedParams = JsonSerializer.Deserialize<RetrieveViewNamesParams>(parameters);
            var views = new View(_organizationService).GetAllEntityViews(deserializedParams.LogicalName, deserializedParams.ViewNamePrefix);

            var viewNames = new List<string>();

            foreach (var entity in views)
            {
                viewNames.Add(entity.Name);
            }

            var res = new RetrieveViewNamesResponse
            {
                ViewNames = viewNames,
            };

            actionResponse.Value = JsonSerializer.Serialize(res);
        }
    }
}
