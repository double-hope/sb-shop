using System;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace SB.Shared.Models.Dynamics
{
	// Do not modify the content of this file.
	// This is an automatically generated file and all 
	// logic should be added in the associated controller class
	// If a controller does not exist, create one that inherits the model.

	public class ProductModel : EntityBase
	{
		// Public static Logical Name
		public const string
			LogicalName = "sb_product";

		#region Attribute Names
		public static class Fields
		{
			public const string
				Article = "sb_article",
				Name = "sb_name",
				PrimaryId = "sb_productid";

			public static string[] All => new[] { Article,
				Name,
				PrimaryId };
		}
		#endregion

		#region Enums
		
		#endregion

		#region Field Definitions
		public string Article
		{
			get => (string)this[Fields.Article];
			set => this[Fields.Article] = value; 
		}
		public string Name
		{
			get => (string)this[Fields.Name];
			set => this[Fields.Name] = value; 
		}
		#endregion

		#region Constructors
		protected ProductModel()
			: base(LogicalName) { }
		protected ProductModel(IOrganizationService service)
			: base(LogicalName, service) { }
		protected ProductModel(Guid id, ColumnSet columnSet, IOrganizationService service)
			: base(service.Retrieve(LogicalName, id, columnSet), service) { }
		protected ProductModel(Guid id, IOrganizationService service)
			: base(LogicalName, id, service) { }
		protected ProductModel(Entity entity, IOrganizationService service)
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