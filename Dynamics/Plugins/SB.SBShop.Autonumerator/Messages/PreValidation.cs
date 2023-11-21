using Microsoft.Xrm.Sdk;
using SB.Shared;
using SB.Shared.EntityProviders;
using SB.Shared.Models;
using SB.Shared.Models.Plugins;
using System;

namespace SB.SBShop.AutonumeratorPlugin.Messages
{
    public class PreValidation : IPlugin
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
                var autoNumerator = new Autonumerator(service).GetByTargetLogicalName(target.LogicalName);
                
                if (autoNumerator == null)
                {
                    autoNumerator = new Autonumerator(service);
                    autoNumerator.CreateAutonumerator(target.LogicalName);
                }
                else
                {
                    autoNumerator.UpdateAutonumerator(autoNumerator);
                }

                var autoNumeratorSerializer = new AutonumeratorSerializer()
                {
                    Prefix = autoNumerator.Prefix,
                    CurrentNumber = autoNumerator.CurrentNumber
                };

                target[EntityParameters.SbNumber] = $"{autoNumerator.Prefix} - {autoNumerator.CurrentNumber + 1}";
                context.SharedVariables.Add("AutoNumerator", JsonSerializer.Serialize(autoNumeratorSerializer));
            }
            catch (Exception e)
            {
                throw new InvalidPluginExecutionException(e.Message);
            }
        }
    }
}
