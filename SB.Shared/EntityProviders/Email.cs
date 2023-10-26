using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using SB.Shared.Models.Dynamics;
using System;

namespace SB.Shared.EntityProviders
{
    public class Email : EmailModel
    {
        public Email(IOrganizationService service) : base(service) { }
        public Email(IOrganizationService service, Guid id) : base(id, service) { }
        public Email(Guid id, ColumnSet columnSet, IOrganizationService service)
                : base(service.Retrieve(LogicalName, id, columnSet), service) { }
        public Email(Entity entity, IOrganizationService service) : base(entity, service) { }

        public Email FillEmail(EntityBase from, EntityBase to)
        {
            var fromPartyCollection = new EntityCollection();
            var fromParty = new ActivityParty(_service)
            {
                PartyId = new EntityReference(from.GetLogicalName(), (Guid)from.Id)
            };
            fromPartyCollection.Entities.Add(fromParty.GetEntity());

            var toPartyCollection = new EntityCollection();
            var toParty = new ActivityParty(_service)
            {
                PartyId = new EntityReference(to.GetLogicalName(), (Guid)to.Id)
            };
            toPartyCollection.Entities.Add(toParty.GetEntity());

            From = fromPartyCollection;
            To = toPartyCollection;
            DirectionCode = true;
            return this;
        }

        public void SendEmailFromTemplate(Guid templateId, Guid regardingId)
        {
            var request = new SendEmailFromTemplateRequest
            {
                Target = GetEntity(),
                TemplateId = templateId,
                RegardingId = regardingId,
                RegardingType = ContactModel.LogicalName,
            };

            _service.Execute(request);
        }
    }
}
