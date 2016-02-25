using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NolWebService.Models
{
    public class Address
    {
        public string ContactName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Province { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
    }
}