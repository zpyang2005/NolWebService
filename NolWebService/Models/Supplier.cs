using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NolWebService.Models
{
    public class Supplier
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int MinimumOrder { get; set; }
        public int MinimumLine { get; set; }
        public string Sale1 { get; set; }
        public string Sale2 { get; set; }
        public Address Address { get; set; }
    }
}