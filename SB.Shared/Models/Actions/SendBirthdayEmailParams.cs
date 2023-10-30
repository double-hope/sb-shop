using System.Runtime.Serialization;

namespace SB.Shared.Models.Actions
{
    [DataContract]
    public class SendBirthdayEmailParams
    {
        [DataMember] public string ContactId { get; set; }
    }
}