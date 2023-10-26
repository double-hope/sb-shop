using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using SB.Shared.Models.Dynamics;
using System;

namespace SB.Shared.EntityProviders
{
    public class Loyaltycard : LoyaltycardModel
    {
        public Loyaltycard(IOrganizationService service) : base(service) { }
        public Loyaltycard(IOrganizationService service, Guid id) : base(id, service) { }
        public Loyaltycard(Guid id, ColumnSet columnSet, IOrganizationService service)
                : base(service.Retrieve(LogicalName, id, columnSet), service) { }
        public Loyaltycard(Entity entity, IOrganizationService service) : base(entity, service) { }

    }
}
