using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using SB.Shared.Extensions;
using SB.Shared.Models.Dynamics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SB.Shared.EntityProviders
{
    public class View : ViewModel
    {
        public View(IOrganizationService service) : base(service) { }
        public View(IOrganizationService service, Guid id) : base(id, service) { }
        public View(Guid id, ColumnSet columnSet, IOrganizationService service)
                : base(service.Retrieve(LogicalName, id, columnSet), service) { }
        public View(Entity entity, IOrganizationService service) : base(entity, service) { }

        public View GetEntityView(string entityName, string viewName)
        {
            var query = new QueryExpression(LogicalName)
            {
                ColumnSet = new ColumnSet(Fields.All),
                Criteria = new FilterExpression
                {
                    FilterOperator = LogicalOperator.And,
                    Conditions =
                    {
                        new ConditionExpression
                        {
                            AttributeName = Fields.Name,
                            Operator = ConditionOperator.Equal,
                            Values = { viewName }
                        },
                        new ConditionExpression
                        {
                            AttributeName = Fields.ReturnedTypeCode,
                            Operator = ConditionOperator.Equal,
                            Values = { entityName }
                        }
                    }
                }
            };

            var view = _service.RetrieveMultiple(query)
                .ToEntityList<View>(_service)
                .FirstOrDefault();

            return view;
        }

        public List<View> GetAllEntityViews(string entityName, string viewNamePrefix)
        {
            var query = new QueryExpression(LogicalName)
            {
                ColumnSet = new ColumnSet(Fields.Name),
                Criteria = new FilterExpression
                {
                    FilterOperator = LogicalOperator.And,
                    Conditions =
                    {
                        new ConditionExpression
                        {
                            AttributeName = Fields.Name,
                            Operator = ConditionOperator.Like,
                            Values = {  "%" + viewNamePrefix + "%" }
                        },
                        new ConditionExpression
                        {
                            AttributeName = Fields.ReturnedTypeCode,
                            Operator = ConditionOperator.Equal,
                            Values = { entityName }
                        }
                    }
                }
            };

            var views = _service.RetrieveMultiple(query).ToEntityList<View>(_service);

            return views;
        }

        public Guid? GetViewGuid(View view)
        {
            return view.Id;
        }

        public string GetFetchXml(View view)
        {
            return view.FetchXml;
        }
        public string GetLayoutXml(View view)
        {
            return view.LayoutXml;
        }
    }
}
