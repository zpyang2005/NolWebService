using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace NolWebService.Models
{
    [DataContract]
    public class Supplier
    {
        [DataMember]
        public string Code { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public int MinimumOrder { get; set; }
        [DataMember]
        public int MinimumLine { get; set; }
        [DataMember]
        public string Sale1 { get; set; }
        [DataMember]
        public string Sale2 { get; set; }
        [DataMember]
        public Address Address { get; set; }
    }
}