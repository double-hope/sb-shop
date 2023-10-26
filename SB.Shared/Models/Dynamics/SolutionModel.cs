using System;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace SB.Shared.Models.Dynamics
{
	// Do not modify the content of this file.
	// This is an automatically generated file and all 
	// logic should be added in the associated controller class
	// If a controller does not exist, create one that inherits the model.

	public class SolutionModel : EntityBase
	{
		// Public static Logical Name
		public const string
			LogicalName = "solution";

		#region Attribute Names
		public static class Fields
		{
			public const string
				PrimaryId = "solutionid",
				PrimaryName = "friendlyname",
				UniqueName = "uniquename";

			public static string[] All => new[] { PrimaryId,
				PrimaryName,
				UniqueName };
		}
		#endregion

		#region Enums
		
		#endregion

		#region Field Definitions
		public string UniqueName
		{
			get => (string)this[Fields.UniqueName];
			set => this[Fields.UniqueName] = value; 
		}
		#endregion

		#region Constructors
		protected SolutionModel()
			: base(LogicalName) { }
		protected SolutionModel(IOrganizationService service)
			: base(LogicalName, service) { }
		protected SolutionModel(Guid id, ColumnSet columnSet, IOrganizationService service)
			: base(service.Retrieve(LogicalName, id, columnSet), service) { }
		protected SolutionModel(Guid id, IOrganizationService service)
			: base(LogicalName, id, service) { }
		protected SolutionModel(Entity entity, IOrganizationService service)
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