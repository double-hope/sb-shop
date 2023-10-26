using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using SB.Shared.Extensions;
using SB.Shared.Models.Dynamics;
using System;
using System.Linq;

namespace SB.Shared.EntityProviders
{
    public class Autonumerator : AutonumeratorModel
    {
        public Autonumerator(IOrganizationService service) : base(service) { }
        public Autonumerator(IOrganizationService service, Guid id) : base(id, service) { }
        public Autonumerator(Guid id, ColumnSet columnSet, IOrganizationService service)
                : base(service.Retrieve(LogicalName, id, columnSet), service) { }
        public Autonumerator(Entity entity, IOrganizationService service) : base(entity, service) { }
        public Autonumerator GetByTargetLogicalName(string targetLogicalName)
        {
            var query = new QueryExpression(LogicalName)
            {
                ColumnSet = new ColumnSet(Fields.CurrentNumber, Fields.Entity, Fields.Prefix),
                Criteria = new FilterExpression
                {
                    Conditions =
                    {
                        new ConditionExpression
                        {
                            AttributeName = Fields.Entity,
                            Operator = ConditionOperator.Equal,
                            Values = { targetLogicalName }
                        }
                    }
                }
            };

            var autonumerator = _service.RetrieveMultiple(query)
                .ToEntityList<Autonumerator>(_service)
                .FirstOrDefault();

            return autonumerator;
        }

        public Autonumerator CreateAutonumerator(string targetLogicalName)
        {
            CurrentNumber = 0;
            Entity = targetLogicalName;
            Prefix = GeneratePrefix(targetLogicalName.Replace("sb_", ""));

            Save();

            return this;
        }

        public Autonumerator UpdateAutonumerator(Autonumerator targetAutonumerator)
        {
            targetAutonumerator.CurrentNumber += 1;
            Save();

            return targetAutonumerator;
        }

        private string GeneratePrefix(string name)
        {
            var prefix = string.Empty;

            for (var i = 0; i < 3; i++)
            {
                var randomChar = new Random().Next(name.Length);
                prefix += name[randomChar];
                name = name.Remove(randomChar, 1);
            }

            return prefix;
        }
    }
}