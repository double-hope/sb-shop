using Microsoft.Xrm.Sdk;
using SB.Shared.Extensions;
using SB.Shared.EntityProviders;
using System;
using SB.Shared.Models.Dynamics;

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
                var autoNumerator = new Autonumerator(service).GetByTargetLogicalName(check.GetLogicalName());

                if (autoNumerator == null)
                {
                    autoNumerator = new Autonumerator(service);
                    autoNumerator.CreateAutonumerator(check.GetLogicalName());
                }
                else
                {
                    autoNumerator.UpdateAutonumerator(autoNumerator);
                }

                target[CheckModel.Fields.Number] = $"{autoNumerator.Prefix} - {autoNumerator.CurrentNumber + 1}";
            }
            catch (Exception e)
            {
                throw new InvalidPluginExecutionException(e.Message);
            }
        }
    }
}
