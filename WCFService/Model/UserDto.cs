using System;
using System.Collections.Generic;

using System.Runtime.Serialization;


namespace WCFService.Model
{
    [DataContract]
    public class UserDto
    {
        [DataMember]
        public int UserId { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Sername { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public List<NotificationActions> Notifications { get; set; }
    }
}
