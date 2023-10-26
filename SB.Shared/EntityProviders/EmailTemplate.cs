using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using SB.Shared.Extensions;
using SB.Shared.Models.Dynamics;
using System;
using System.Linq;

namespace SB.Shared.EntityProviders
{
    public class EmailTemplate : EmailTemplateModel
    {
        public EmailTemplate(IOrganizationService service) : base(service) { }
        public EmailTemplate(IOrganizationService service, Guid id) : base(id, service) { }
        public EmailTemplate(Guid id, ColumnSet columnSet, IOrganizationService service)
                : base(service.Retrieve(LogicalName, id, columnSet), service) { }
        public EmailTemplate(Entity entity, IOrganizationService service) : base(entity, service) { }

        public bool IsTemplateExists(Guid id)
        {
            var query = new QueryExpression(LogicalName)
            {
                ColumnSet = new ColumnSet(Fields.PrimaryId),
                Criteria = new FilterExpression
                {
                    Conditions =
                    {
                        new ConditionExpression
                        {
                            AttributeName = Fields.PrimaryId,
                            Operator = ConditionOperator.Equal,
                            Values = { id }
                        }
                    }
                }
            };

            var template = _service.RetrieveMultiple(query)
                .ToEntityList<EmailTemplate>(_service)
                .FirstOrDefault();

            return template != null;
        }
    }
}
