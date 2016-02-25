using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NolWebService.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public string CategoryName { get; set; }
        public string PartNumberAbbott { get; set; }
        public string AbbottDescription { get; set; }
        public string SizeText { get; set; }
        public string DescriptionText { get; set; }
        public string WebNoteText { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string PriceUOM { get; set; }
        public bool IsPriceOffered { get; set; }
        public bool Certified { get; set; }
        public bool RoHS { get; set; }
        public Supplier Supplier { get; set; }
        public ProductAttribute Attribute { get; set; }
    }
}