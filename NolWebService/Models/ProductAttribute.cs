using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NolWebService.Models
{
    public class ProductAttribute
    {
        public string MeasureSystem { get; set; }
        public string Size { get; set; }
        public string Length { get; set; }
        public decimal LengthValue { get; set; }
        public string Head { get; set; }
        public string Point { get; set; }
        public string Drive { get; set; }
        public string Style { get; set; }
        public string Material { get; set; }
        public string Finish { get; set; }
        public string Thickness { get; set; }
        public string OutDiameter { get; set; }
    }

}
