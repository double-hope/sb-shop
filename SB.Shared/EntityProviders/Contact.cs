using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using SB.Shared.Extensions;
using SB.Shared.Models.Dynamics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SB.Shared.EntityProviders
{
    public class Contact : ContactModel
    {
        public Contact(IOrganizationService service) : base(service) { }
        public Contact(IOrganizationService service, Guid id) : base(id, service) { }
        public Contact(Guid id, ColumnSet columnSet, IOrganizationService service)
                : base(service.Retrieve(LogicalName, id, columnSet), service) { }
        public Contact(Entity entity, IOrganizationService service) : base(entity, service) { }

        public Contact GetDublicatedPhoneUser(string phoneNumber)
        {
            var query = new QueryExpression(LogicalName)
            {
                ColumnSet = new ColumnSet(Fields.MobilePhone),
                Criteria = new FilterExpression
                {
                    Conditions =
                    {
                        new ConditionExpression
                        {
                            AttributeName = Fields.MobilePhone,
                            Operator = ConditionOperator.Equal,
                            Values = {  phoneNumber }
                        }
                    }
                }
            };

            var contact = _service.RetrieveMultiple(query)
                .ToEntityList<Contact>(_service)
                .FirstOrDefault();

            return contact;
        }

        public List<Contact> GetContacts(FilterExpression filter = null, params string[] columns)
        {
            var query = new QueryExpression(LogicalName)
            {
                ColumnSet = new ColumnSet(columns.Length > 0 ? columns : Fields.All)
            };

            if (filter != null) query.Criteria = filter;

            var contacts = _service.RetrieveMultiple(query).ToEntityList<Contact>(_service);

            return contacts;
        }

        public Contact GetContactById(Guid contactId, params string[] columns)
        {
            var query = new QueryExpression(LogicalName)
            {
                ColumnSet = new ColumnSet(columns.Length > 0 ? columns : Fields.All),
                Criteria = new FilterExpression
                {
                    Conditions =
                    {
                        new ConditionExpression
                        {
                            AttributeName = Fields.PrimaryId,
                            Operator = ConditionOperator.Equal,
                            Values = { contactId }
                        }
                    }
                }
            };

            var contact = _service.RetrieveMultiple(query)
                .ToEntityList<Contact>(_service)
                .FirstOrDefault();

            return contact;
        }

        public List<Contact> GetContactsBirthdayToday()
        {
            var date = DateTime.Now.ToString("dd.MM");

            var filter = new FilterExpression
            {
                Conditions =
                {
                    new ConditionExpression
                    {
                        AttributeName = Fields.Birthdaythisyear,
                        Operator = ConditionOperator.Equal,
                        Values = { date }
                    }
                }
            };

            var query = new QueryExpression(LogicalName)
            {
                ColumnSet = new ColumnSet(Fields.Birthdaythisyear),
                Criteria = filter
            };

            var contacts = _service.RetrieveMultiple(query)
               .ToEntityList<Contact>(_service);

            return contacts;
        }
    }
}