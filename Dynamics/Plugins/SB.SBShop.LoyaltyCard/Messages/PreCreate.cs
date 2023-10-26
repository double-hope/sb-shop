using Microsoft.Xrm.Sdk;
using SB.Shared.EntityProviders;
using SB.Shared.Extensions;
using SB.Shared.Models.Dynamics;
using System;

namespace SB.SBShop.LoyaltyCard.Messages
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
                var loyaltyCard = target.ToEntity<Loyaltycard>(service);
                var autoNumerator = new Autonumerator(service).GetByTargetLogicalName(loyaltyCard.GetLogicalName());

                if (autoNumerator == null)
                {
                    autoNumerator = new Autonumerator(service);
                    autoNumerator.CreateAutonumerator(loyaltyCard.GetLogicalName());
                }
                else
                {
                    autoNumerator.UpdateAutonumerator(autoNumerator);
                }

                target[LoyaltycardModel.Fields.Number] = $"{autoNumerator.Prefix} - {autoNumerator.CurrentNumber + 1}";

                if(loyaltyCard.Clientid != null)
                {
                    var contact = new Contact(service).GetContactById(loyaltyCard.Clientid.Id);
                    target[LoyaltycardModel.Fields.Firstname] = contact.FirstName;
                    target[LoyaltycardModel.Fields.Lastname] = contact.LastName;
                    target[LoyaltycardModel.Fields.Email] = contact.EMailAddress1;
                    target[LoyaltycardModel.Fields.Phone] = contact.MobilePhone;
                }
            }
            catch (Exception e)
            {
                throw new InvalidPluginExecutionException(e.Message);
            }
        }
    }
}
