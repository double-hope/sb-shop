using System;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace SB.Shared.Models.Dynamics
{
	// Do not modify the content of this file.
	// This is an automatically generated file and all 
	// logic should be added in the associated controller class
	// If a controller does not exist, create one that inherits the model.

	public class ViewModel : EntityBase
	{
		// Public static Logical Name
		public const string
			LogicalName = "savedquery";

		#region Attribute Names
		public static class Fields
		{
			public const string
				FetchXml = "fetchxml",
				LayoutXml = "layoutxml",
				Name = "name",
				PrimaryId = "savedqueryid",
				ReturnedTypeCode = "returnedtypecode";

			public static string[] All => new[] { FetchXml,
				LayoutXml,
				Name,
				PrimaryId,
				ReturnedTypeCode };
		}
		#endregion

		#region Enums
		
		#endregion

		#region Field Definitions
		public string FetchXml
		{
			get => (string)this[Fields.FetchXml];
			set => this[Fields.FetchXml] = value; 
		}
		public string LayoutXml
		{
			get => (string)this[Fields.LayoutXml];
			set => this[Fields.LayoutXml] = value; 
		}
		public string Name
		{
			get => (string)this[Fields.Name];
			set => this[Fields.Name] = value; 
		}
		public string ReturnedTypeCode
		{
			get => (string)this[Fields.ReturnedTypeCode];
			set => this[Fields.ReturnedTypeCode] = value; 
		}
		#endregion

		#region Constructors
		protected ViewModel()
			: base(LogicalName) { }
		protected ViewModel(IOrganizationService service)
			: base(LogicalName, service) { }
		protected ViewModel(Guid id, ColumnSet columnSet, IOrganizationService service)
			: base(service.Retrieve(LogicalName, id, columnSet), service) { }
		protected ViewModel(Guid id, IOrganizationService service)
			: base(LogicalName, id, service) { }
		protected ViewModel(Entity entity, IOrganizationService service)
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