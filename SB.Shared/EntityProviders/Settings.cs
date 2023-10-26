using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using SB.Shared.Extensions;
using SB.Shared.Models.Dynamics;
using System;
using System.Linq;
using System.Text;

namespace SB.Shared.Dynamics.EntityProviders
{
    public class Settings : SBCustomSettingsModel
    {
        public Settings(IOrganizationService service) : base(service) { }
        public Settings(IOrganizationService service, Guid id) : base(id, service) { }
        public Settings(Guid id, ColumnSet columnSet, IOrganizationService service)
            : base(service.Retrieve(LogicalName, id, columnSet), service) { }
        public Settings(Entity entity, IOrganizationService service) : base(entity, service) { }

        public Settings GetSettings(params string[] columns)
        {
            var query = new QueryExpression(LogicalName)
            {
                ColumnSet = new ColumnSet(columns)
            };
            var settings = _service.RetrieveMultiple(query)
                .ToEntityList<Settings>(_service)
                .FirstOrDefault();

            if (settings == null)
            {
                throw new InvalidOperationException("SB Custom settings not found. Please configure system or contact the system administrator for support.");
            }

            return settings;
        }

        private Settings GetDirectionSettings(string directionName)
        {
            var query = new QueryExpression(LogicalName)
            {
                ColumnSet = new ColumnSet(Fields.All.Where(s => s.StartsWith(directionName)).ToArray())
            };

            var settings = _service.RetrieveMultiple(query).Entities.FirstOrDefault();

            return settings == null ? null : new Settings(settings, _service);
        }
        public static string GetBasicHeader(string userName, string userPassword)
        {
            return "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes(userName + ":" + userPassword));
        }

        public Guid GetTemplateGuid()
        {
            var query = new QueryExpression(LogicalName)
            {
                ColumnSet = new ColumnSet(Fields.Emailtemplateid)
            };

            var settings = _service.RetrieveMultiple(query)
                .ToEntityList<Settings>(_service)
                .FirstOrDefault(x => !string.IsNullOrEmpty(x.Emailtemplateid));

            return new Guid(settings.Emailtemplateid);
        }
    }
}