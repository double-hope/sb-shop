using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SB.Shared.Models.Actions
{
    [DataContract]
    public class RetrieveLogicalNamesParams
    {
        [DataMember] public string LogicalNamePrefix { get; set; }
    }

    [DataContract]
    public class RetrieveLogicalNamesResponse
    {
        [DataMember] public List<string> LogicalNames;
    }
}