using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using SB.Shared.Dynamics.EntityProviders;
using SB.Shared.Extensions;
using SB.Shared.Models.Actions;
using SB.Shared.Models.Dynamics;
using System;

namespace SB.Shared.EntityProviders
{
    public class User : UserModel
    {
        public User(IOrganizationService service) : base(service) { }
        public User(IOrganizationService service, Guid id) : base(id, service) { }
        public User(Guid id, ColumnSet columnSet, IOrganizationService service)
                : base(service.Retrieve(LogicalName, id, columnSet), service) { }
        public User(Entity entity, IOrganizationService service) : base(entity, service) { }

        public string GetEmail()
        {
            var userEntity = _service.Retrieve(LogicalName, (Guid)Id, new ColumnSet(Fields.InternalEMailAddress, Fields.PrimaryId));

            var user = userEntity.ToEntity<User>(_service);

            return user.InternalEMailAddress;
        }
        
        public void SendFileToUser(View view, Settings settings)
        {
            var email = GetEmail();
            var parameterCollection = new ParameterCollection
            {
                { "View", new EntityReference(ViewModel.LogicalName, (Guid)view.Id) },
                { "FetchXml", view.FetchXml },
                { "LayoutXml", view.LayoutXml },
                { "QueryApi", "" },
                { "QueryParameters", new InputArgumentCollection() },
            };

            var organizationRequest = new OrganizationRequest("ExportToExcel")
            {
                Parameters = parameterCollection,
            };

            var organizationResponse = _service.Execute(organizationRequest);

            var sendExcelRequest = new SendExcelFlowRequestParams
            {
                email = email,
                file = Convert.ToBase64String(organizationResponse.Results["ExcelFile"] as byte[]),
            };

            if (!string.IsNullOrEmpty(settings.Powerautomatelink))
            {
                var body = JsonSerializer.Serialize(sendExcelRequest);
                WebUtilities.Post(settings.Powerautomatelink, body);
            }
        }
    }
}
