using Microsoft.Xrm.Sdk;
using SB.Shared;
using SB.Shared.Extensions;
using SB.Shared.Models.Dynamics;
using SB.Shared.Models.Plugins;
using System;

namespace SB.SBShop.Check.Messages
{
    public class PreCreate : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            var tracer = (ITracingService)serviceProvider.GetService(typeof(ITracingService));
            var context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            var factory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            var service = factory.CreateOrganizationService(context.UserId);

            try
            {
                var target = (Entity)context.InputParameters["Target"];
                var check = target.ToEntity<Shared.EntityProviders.Check>(service);
                if (context.ParentContext.SharedVariables.Contains("AutoNumerator"))
                {
                    var autoNumerator = JsonSerializer.Deserialize<AutonumeratorSerializer>(context.ParentContext.SharedVariables["AutoNumerator"].ToString());
                    target[CheckModel.Fields.PrimaryName] = $"{autoNumerator.Prefix} - {autoNumerator.CurrentNumber + 1}";
                }
            }
            catch (Exception e)
            {
                throw new InvalidPluginExecutionException(e.Message);
            }
        }
    }
}