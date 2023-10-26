using System;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace SB.Shared.Models.Dynamics
{
	// Do not modify the content of this file.
	// This is an automatically generated file and all 
	// logic should be added in the associated controller class
	// If a controller does not exist, create one that inherits the model.

	public class SolutionComponentModel : EntityBase
	{
		// Public static Logical Name
		public const string
			LogicalName = "solutioncomponent";

		#region Attribute Names
		public static class Fields
		{
			public const string
				ComponentType = "componenttype",
                ObjectId = "objectid",
                PrimaryId = "solutioncomponentid",
				PrimaryName = "";

			public static string[] All => new[] { ComponentType,
				PrimaryId,
				PrimaryName };
		}
		#endregion

		#region Enums
		public static class ComponentTypeEnum
		{
			public const int
				_1 = 1,
				_2 = 2,
				_3 = 3,
				_4 = 4,
				_5 = 5,
				_6 = 6,
				_7 = 7,
				_8 = 8,
				_9 = 9,
				_10 = 10,
				_11 = 11,
				_12 = 12,
				_13 = 13,
				_14 = 14,
				_16 = 16,
				_17 = 17,
				_20 = 20,
				_21 = 21,
				_22 = 22,
				_23 = 23,
				_24 = 24,
				_25 = 25,
				_26 = 26,
				_29 = 29,
				_31 = 31,
				_32 = 32,
				_33 = 33,
				_34 = 34,
				_35 = 35,
				_36 = 36,
				_37 = 37,
				_38 = 38,
				_39 = 39,
				_44 = 44,
				_45 = 45,
				_46 = 46,
				_47 = 47,
				_48 = 48,
				_49 = 49,
				_50 = 50,
				_52 = 52,
				_53 = 53,
				_55 = 55,
				_59 = 59,
				_60 = 60,
				_61 = 61,
				_62 = 62,
				_63 = 63,
				_64 = 64,
				_70 = 70,
				_71 = 71,
				_90 = 90,
				_91 = 91,
				_92 = 92,
				_93 = 93,
				_95 = 95,
				_150 = 150,
				_151 = 151,
				_152 = 152,
				_153 = 153,
				_154 = 154,
				_155 = 155,
				_65 = 65,
				_161 = 161,
				_162 = 162,
				_165 = 165,
				_66 = 66,
				_68 = 68,
				_166 = 166,
				_201 = 201,
				_202 = 202,
				_203 = 203,
				_204 = 204,
				_205 = 205,
				_206 = 206,
				_207 = 207,
				_210 = 210,
				_18 = 18,
				_208 = 208,
				_300 = 300,
				_371 = 371,
				_372 = 372,
				_380 = 380,
				_381 = 381,
				_400 = 400,
				_401 = 401,
				_402 = 402,
				_430 = 430,
				_431 = 431,
				_432 = 432;
		}
		#endregion

		#region Field Definitions
		public OptionSetValue ComponentType
		{
			get => (OptionSetValue)this[Fields.ComponentType];
			set => this[Fields.ComponentType] = value; 
		}
		#endregion

		#region Constructors
		protected SolutionComponentModel()
			: base(LogicalName) { }
		protected SolutionComponentModel(IOrganizationService service)
			: base(LogicalName, service) { }
		protected SolutionComponentModel(Guid id, ColumnSet columnSet, IOrganizationService service)
			: base(service.Retrieve(LogicalName, id, columnSet), service) { }
		protected SolutionComponentModel(Guid id, IOrganizationService service)
			: base(LogicalName, id, service) { }
		protected SolutionComponentModel(Entity entity, IOrganizationService service)
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