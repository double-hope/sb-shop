using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using SB.Shared.Models.Dynamics;
using System;

namespace SB.Shared.EntityProviders
{
    public class Check : CheckModel
    {
        public Check(IOrganizationService service) : base(service) { }
        public Check(IOrganizationService service, Guid id) : base(id, service) { }
        public Check(Guid id, ColumnSet columnSet, IOrganizationService service)
                : base(service.Retrieve(LogicalName, id, columnSet), service) { }
        public Check(Entity entity, IOrganizationService service) : base(entity, service) { }
    }
}