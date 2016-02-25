using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace NolWebService.Models
{
    [DataContract]
    public class Product
    {
        [DataMember]
        public int ProductID { get; set; }
        [DataMember]
        public string CategoryName { get; set; }
        [DataMember]
        public string PartNumberAbbott { get; set; }
        [DataMember]
        public string AbbottDescription { get; set; }
        [DataMember]
        public string SizeText { get; set; }
        [DataMember]
        public string DescriptionText { get; set; }
        [DataMember]
        public string WebNoteText { get; set; }
        [DataMember]
        public int Quantity { get; set; }
        [DataMember]
        public decimal Price { get; set; }
        [DataMember]
        public string PriceUOM { get; set; }
        [DataMember]
        public bool IsPriceOffered { get; set; }
        [DataMember]
        public bool Certified { get; set; }
        [DataMember]
        public bool RoHS { get; set; }
        [DataMember]
        public Supplier Supplier { get; set; }
        [DataMember]
        public ProductAttribute Attribute { get; set; }
    }
}