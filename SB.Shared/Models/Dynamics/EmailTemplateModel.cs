using System;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace SB.Shared.Models.Dynamics
{
	// Do not modify the content of this file.
	// This is an automatically generated file and all 
	// logic should be added in the associated controller class
	// If a controller does not exist, create one that inherits the model.

	public class EmailTemplateModel : EntityBase
	{
		// Public static Logical Name
		public const string
			LogicalName = "template";

		#region Attribute Names
		public static class Fields
		{
			public const string
				PrimaryId = "templateid",
				PrimaryName = "title",
				TemplateTypeCode = "templatetypecode";

			public static string[] All => new[] { PrimaryId,
				PrimaryName,
				TemplateTypeCode };
		}
		#endregion

		#region Enums
		
		#endregion

		#region Field Definitions
		public string TemplateTypeCode
		{
			get => (string)this[Fields.TemplateTypeCode];
			set => this[Fields.TemplateTypeCode] = value; 
		}
		#endregion

		#region Constructors
		protected EmailTemplateModel()
			: base(LogicalName) { }
		protected EmailTemplateModel(IOrganizationService service)
			: base(LogicalName, service) { }
		protected EmailTemplateModel(Guid id, ColumnSet columnSet, IOrganizationService service)
			: base(service.Retrieve(LogicalName, id, columnSet), service) { }
		protected EmailTemplateModel(Guid id, IOrganizationService service)
			: base(LogicalName, id, service) { }
		protected EmailTemplateModel(Entity entity, IOrganizationService service)
			: base(entity, service) { }
		#endregion

		#region Public Methods
		public override string GetPrimaryAttribute()
        {
            return Fields.PrimaryId;
        }
		#endregion
	}
}