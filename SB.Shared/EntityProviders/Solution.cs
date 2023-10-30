using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using SB.Shared.Extensions;
using SB.Shared.Models.Dynamics;
using System;
using System.Linq;

namespace SB.Shared.EntityProviders
{
    public class Solution : SolutionModel
    {
        public Solution(IOrganizationService service) : base(service) { }
        public Solution(IOrganizationService service, Guid id) : base(id, service) { }
        public Solution(Guid id, ColumnSet columnSet, IOrganizationService service)
                : base(service.Retrieve(LogicalName, id, columnSet), service) { }
        public Solution(Entity entity, IOrganizationService service) : base(entity, service) { }

        public Solution GetCurrentSolution(string solutionName)
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
                            AttributeName = Fields.UniqueName,
                            Operator = ConditionOperator.Equal,
                            Values = { solutionName }
                        }
                    }
                }
            };

            var solution = _service.RetrieveMultiple(query)
                .ToEntityList<Solution>(_service)
                .FirstOrDefault();

            return solution;
        }
    }
}