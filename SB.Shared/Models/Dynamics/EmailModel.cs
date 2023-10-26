using System;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace SB.Shared.Models.Dynamics
{
	// Do not modify the content of this file.
	// This is an automatically generated file and all 
	// logic should be added in the associated controller class
	// If a controller does not exist, create one that inherits the model.

	public class EmailModel : EntityBase
	{
		// Public static Logical Name
		public const string
			LogicalName = "email";

		#region Attribute Names
		public static class Fields
		{
			public const string
				DirectionCode = "directioncode",
				From = "from",
				PrimaryId = "activityid",
				PrimaryName = "subject",
				To = "to";

			public static string[] All => new[] { DirectionCode,
				From,
				PrimaryId,
				PrimaryName,
				To };
		}
		#endregion

		#region Enums
		
		#endregion

		#region Field Definitions
		public bool? DirectionCode
		{
			get => (bool?)this[Fields.DirectionCode];
			set => this[Fields.DirectionCode] = value; 
		}
		public EntityCollection From
		{
			get => (EntityCollection)this[Fields.From];
			set => this[Fields.From] = value; 
		}
		public EntityCollection To
		{
			get => (EntityCollection)this[Fields.To];
			set => this[Fields.To] = value; 
		}
		#endregion

		#region Constructors
		protected EmailModel()
			: base(LogicalName) { }
		protected EmailModel(IOrganizationService service)
			: base(LogicalName, service) { }
		protected EmailModel(Guid id, ColumnSet columnSet, IOrganizationService service)
			: base(service.Retrieve(LogicalName, id, columnSet), service) { }
		protected EmailModel(Guid id, IOrganizationService service)
			: base(LogicalName, id, service) { }
		protected EmailModel(Entity entity, IOrganizationService service)
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