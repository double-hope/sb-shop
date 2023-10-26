using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SB.Shared.Models.Actions
{
    [DataContract]
    public class RetrieveViewNamesParams
    {
        [DataMember] public string LogicalName { get; set; }
        [DataMember] public string ViewNamePrefix { get; set; }
    }

    [DataContract]
    public class RetrieveViewNamesResponse
    {
        [DataMember] public List<string> ViewNames;
    }
}
