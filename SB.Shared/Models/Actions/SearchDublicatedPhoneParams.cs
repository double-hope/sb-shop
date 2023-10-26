using System;
using System.Runtime.Serialization;

namespace SB.Shared.Models.Actions
{
    [DataContract]
    public class SearchDublicatedPhoneParams
    {
        [DataMember] public string PhoneNumber;
    }

    [DataContract]
    public class SearchDublicatedPhoneResponse
    {
        [DataMember] public Guid? DublicatedUserId;
    }
}
