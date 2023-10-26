using System;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace SB.Shared.Models.Dynamics
{
	// Do not modify the content of this file.
	// This is an automatically generated file and all 
	// logic should be added in the associated controller class
	// If a controller does not exist, create one that inherits the model.

	public class SBCustomSettingsModel : EntityBase
	{
		// Public static Logical Name
		public const string
			LogicalName = "sb_customsettings";

		#region Attribute Names
		public static class Fields
		{
			public const string
				Emailtemplateid = "sb_emailtemplateid",
				Name = "sb_name",
				Powerautomatelink = "sb_powerautomatelink",
				PrimaryId = "sb_customsettingsid",
				Solutionname = "sb_solutionname";

			public static string[] All => new[] { Emailtemplateid,
				Name,
				Powerautomatelink,
				PrimaryId,
				Solutionname };
		}
		#endregion

		#region Enums
		
		#endregion

		#region Field Definitions
		public string Emailtemplateid
		{
			get => (string)this[Fields.Emailtemplateid];
			set => this[Fields.Emailtemplateid] = value; 
		}
		public string Name
		{
			get => (string)this[Fields.Name];
			set => this[Fields.Name] = value; 
		}
		public string Powerautomatelink
		{
			get => (string)this[Fields.Powerautomatelink];
			set => this[Fields.Powerautomatelink] = value; 
		}
		public string Solutionname
		{
			get => (string)this[Fields.Solutionname];
			set => this[Fields.Solutionname] = value; 
		}
		#endregion

		#region Constructors
		protected SBCustomSettingsModel()
			: base(LogicalName) { }
		protected SBCustomSettingsModel(IOrganizationService service)
			: base(LogicalName, service) { }
		protected SBCustomSettingsModel(Guid id, ColumnSet columnSet, IOrganizationService service)
			: base(service.Retrieve(LogicalName, id, columnSet), service) { }
		protected SBCustomSettingsModel(Guid id, IOrganizationService service)
			: base(LogicalName, id, service) { }
		protected SBCustomSettingsModel(Entity entity, IOrganizationService service)
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