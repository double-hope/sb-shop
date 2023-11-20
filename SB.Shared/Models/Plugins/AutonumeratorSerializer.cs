using System.Runtime.Serialization;

namespace SB.Shared.Models.Plugins
{
    public class AutonumeratorSerializer
    {
        [DataMember] public string Prefix { get; set; }
        [DataMember] public int? CurrentNumber { get; set; }
    }
}
