using System;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace SB.Shared.Models.Dynamics
{
	// Do not modify the content of this file.
	// This is an automatically generated file and all 
	// logic should be added in the associated controller class
	// If a controller does not exist, create one that inherits the model.

	public class CheckProductModel : EntityBase
	{
		// Public static Logical Name
		public const string
			LogicalName = "sb_checkproduct";

		#region Attribute Names
		public static class Fields
		{
			public const string
				Checkid = "sb_checkid",
				Cost = "sb_cost",
				Discount = "sb_discount",
				PrimaryId = "sb_checkproductid",
				PrimaryName = "sb_name",
				Productid = "sb_productid",
				Totalcost = "sb_totalcost";

			public static string[] All => new[] { Checkid,
				Cost,
				Discount,
				PrimaryId,
				PrimaryName,
				Productid,
				Totalcost };
		}
		#endregion

		#region Enums
		
		#endregion

		#region Field Definitions
		public EntityReference Checkid
		{
			get => (EntityReference)this[Fields.Checkid];
			set => this[Fields.Checkid] = value; 
		}
		public decimal? Cost
		{
			get => (decimal?)this[Fields.Cost];
			set => this[Fields.Cost] = value; 
		}
		public decimal? Discount
		{
			get => (decimal?)this[Fields.Discount];
			set => this[Fields.Discount] = value; 
		}
		public EntityReference Productid
		{
			get => (EntityReference)this[Fields.Productid];
			set => this[Fields.Productid] = value; 
		}
		public decimal? Totalcost
		{
			get => (decimal?)this[Fields.Totalcost];
			set => this[Fields.Totalcost] = value; 
		}
		#endregion

		#region Constructors
		protected CheckProductModel()
			: base(LogicalName) { }
		protected CheckProductModel(IOrganizationService service)
			: base(LogicalName, service) { }
		protected CheckProductModel(Guid id, ColumnSet columnSet, IOrganizationService service)
			: base(service.Retrieve(LogicalName, id, columnSet), service) { }
		protected CheckProductModel(Guid id, IOrganizationService service)
			: base(LogicalName, id, service) { }
		protected CheckProductModel(Entity entity, IOrganizationService service)
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