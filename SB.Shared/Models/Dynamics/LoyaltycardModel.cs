using System;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace SB.Shared.Models.Dynamics
{
	// Do not modify the content of this file.
	// This is an automatically generated file and all 
	// logic should be added in the associated controller class
	// If a controller does not exist, create one that inherits the model.

	public class LoyaltycardModel : EntityBase
	{
		// Public static Logical Name
		public const string
			LogicalName = "sb_loyaltycard";

		#region Attribute Names
		public static class Fields
		{
			public const string
				Clientid = "sb_clientid",
				Email = "sb_email",
				Firstname = "sb_firstname",
				Lastname = "sb_lastname",
				Number = "sb_number",
				Phone = "sb_phone",
				PrimaryId = "sb_loyaltycardid",
				PrimaryName = "sb_name";

			public static string[] All => new[] { Clientid,
				Email,
				Firstname,
				Lastname,
				Number,
				Phone,
				PrimaryId,
				PrimaryName };
		}
		#endregion

		#region Enums
		
		#endregion

		#region Field Definitions
		public EntityReference Clientid
		{
			get => (EntityReference)this[Fields.Clientid];
			set => this[Fields.Clientid] = value; 
		}
		public string Email
		{
			get => (string)this[Fields.Email];
			set => this[Fields.Email] = value; 
		}
		public string Firstname
		{
			get => (string)this[Fields.Firstname];
			set => this[Fields.Firstname] = value; 
		}
		public string Lastname
		{
			get => (string)this[Fields.Lastname];
			set => this[Fields.Lastname] = value; 
		}
		public string Number
		{
			get => (string)this[Fields.Number];
			set => this[Fields.Number] = value; 
		}
		public string Phone
		{
			get => (string)this[Fields.Phone];
			set => this[Fields.Phone] = value; 
		}
		#endregion

		#region Constructors
		protected LoyaltycardModel()
			: base(LogicalName) { }
		protected LoyaltycardModel(IOrganizationService service)
			: base(LogicalName, service) { }
		protected LoyaltycardModel(Guid id, ColumnSet columnSet, IOrganizationService service)
			: base(service.Retrieve(LogicalName, id, columnSet), service) { }
		protected LoyaltycardModel(Guid id, IOrganizationService service)
			: base(LogicalName, id, service) { }
		protected LoyaltycardModel(Entity entity, IOrganizationService service)
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