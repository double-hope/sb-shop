using Microsoft.Xrm.Sdk;
using SB.Shared;
using SB.Shared.Dynamics.EntityProviders;
using SB.Shared.EntityProviders;
using SB.Shared.Models;
using SB.Shared.Models.Actions;
using SB.Shared.Models.Dynamics;
using System.Collections.Generic;

namespace SB.Actions.Messages
{
    public class RetrieveLogicalNames : IActionTracking
    {
        private readonly IOrganizationService _organizationService;
        private readonly IPluginExecutionContext _context;

        public RetrieveLogicalNames(IOrganizationService organizationService, IPluginExecutionContext context)
        {
            _organizationService = organizationService;
            _context = context;
        }

        public void Execute(string parameters, ref ActionResponse actionResponse)
        {
            var deserializedParams = JsonSerializer.Deserialize<RetrieveLogicalNamesParams>(parameters);

            var settings = new Settings(_organizationService).GetSettings(SBCustomSettingsModel.Fields.Solutionname);

            var solutionEntities = new SolutionComponent(_organizationService)
                .GetSolutionEntities(settings.Solutionname, deserializedParams.LogicalNamePrefix);

            var logicalNames = new List<string>();

            foreach (var entity in solutionEntities.Entities)
            {
                logicalNames.Add(entity.GetAttributeValue<string>(EntityParameters.LogicalName));
            }

            var res = new RetrieveLogicalNamesResponse
            {
                LogicalNames = logicalNames,
            };

            actionResponse.Value = JsonSerializer.Serialize(res);
        }
    }
}