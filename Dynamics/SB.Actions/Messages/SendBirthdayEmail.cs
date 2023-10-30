using Microsoft.Xrm.Sdk;
using SB.Shared;
using SB.Shared.Dynamics.EntityProviders;
using SB.Shared.EntityProviders;
using SB.Shared.Models.Actions;
using System;

namespace SB.Actions.Messages
{
    public class SendBirthdayEmail : IActionTracking
    {
        private readonly IOrganizationService _organizationService;
        private readonly IPluginExecutionContext _context;

        public SendBirthdayEmail(IOrganizationService organizationService, IPluginExecutionContext context)
        {
            _organizationService = organizationService;
            _context = context;
        }

        public void Execute(string parameters, ref ActionResponse actionResponse)
        {
            var deserializedParams = JsonSerializer.Deserialize<SendBirthdayEmailParams>(parameters);

            var user = new User(_organizationService, _context.UserId);
            var contact = new Contact(_organizationService).GetContactById(new Guid(deserializedParams.ContactId));

            if (contact == null) return;

            var email = new Email(_organizationService)
                .FillEmail(user, contact);

            var templateId = new Settings(_organizationService).GetTemplateGuid();

            var isTemplateExists = new EmailTemplate(_organizationService)
                .IsTemplateExists(templateId);

            if (!isTemplateExists) return;

            email.SendEmailFromTemplate(templateId, new Guid(deserializedParams.ContactId));
        }
    }
}