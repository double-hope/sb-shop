using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using SB.Shared.Extensions;
using SB.Shared.Models.Dynamics;
using System;
using System.Linq;

namespace SB.Shared.EntityProviders
{
    public class SolutionComponent : SolutionComponentModel
    {
        public SolutionComponent(IOrganizationService service) : base(service) { }
        public SolutionComponent(IOrganizationService service, Guid id) : base(id, service) { }
        public SolutionComponent(Guid id, ColumnSet columnSet, IOrganizationService service)
                : base(service.Retrieve(LogicalName, id, columnSet), service) { }
        public SolutionComponent(Entity entity, IOrganizationService service) : base(entity, service) { }

        public EntityCollection GetSolutionEntities(string solutionName, string logicalNamePrefix)
        {
            var solution = new Solution(_service).GetCurrentSolution(solutionName);

            QueryExpression query = new QueryExpression(LogicalName)
            {
                ColumnSet = new ColumnSet(Fields.ObjectId),
                Criteria = new FilterExpression()
                {
                    FilterOperator = LogicalOperator.And,
                    Conditions =
                    {
                        new ConditionExpression()
                        {
                            AttributeName = SolutionModel.Fields.PrimaryId,
                            Operator = ConditionOperator.Equal,
                            Values = { solution.Id }
                        },
                        new ConditionExpression()
                        {
                            AttributeName = Fields.ComponentType,
                            Operator = ConditionOperator.Equal,
                            Values = { ComponentTypeEnum._1 }
                        }
                    }
                }
            };

            var response = _service.RetrieveMultiple(query);
            var entityColection = new EntityCollection();

            foreach (var entity in response.Entities)
            {
                var entityId = entity.GetAttributeValue<Guid>(Fields.ObjectId);

                var entityQuery = new QueryExpression()
                {
                    EntityName = "entity",
                    ColumnSet = new ColumnSet("logicalname"),
                    Criteria = new FilterExpression(LogicalOperator.And)
                    {
                        FilterOperator = LogicalOperator.And,
                        Conditions =
                            {
                                new ConditionExpression("entityid", ConditionOperator.Equal, entityId),
                                new ConditionExpression("logicalname", ConditionOperator.Like, "%" + logicalNamePrefix + "%"),
                            },
                    }
                };

                var res = _service.RetrieveMultiple(entityQuery).Entities.FirstOrDefault();
                if (res != null) entityColection.Entities.Add(res);
            }

            return entityColection;
        }
    }
}