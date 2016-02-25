using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace NolWebService.Models
{
    [DataContract]
    public class ProductAttribute
    {
        [DataMember]
        public string MeasureSystem { get; set; }
        [DataMember]
        public string Size { get; set; }
        [DataMember]
        public string Length { get; set; }
        [DataMember]
        public decimal LengthValue { get; set; }
        [DataMember]
        public string Head { get; set; }
        [DataMember]
        public string Point { get; set; }
        [DataMember]
        public string Drive { get; set; }
        [DataMember]
        public string Style { get; set; }
        [DataMember]
        public string Material { get; set; }
        [DataMember]
        public string Finish { get; set; }
        [DataMember]
        public string Thickness { get; set; }
        [DataMember]
        public string OutDiameter { get; set; }
    }

}
