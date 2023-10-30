using System.Runtime.Serialization;

namespace SB.Shared.Models.Actions
{
    [DataContract]
    public class SendExcelFlowParams
    {
        [DataMember] public string ViewName { get; set; }
        [DataMember] public string EntityName { get; set; }
    }

    [DataContract]
    public class SendExcelFlowRequestParams
    {
        [DataMember] public string email;
        [DataMember] public string file;
    }
}