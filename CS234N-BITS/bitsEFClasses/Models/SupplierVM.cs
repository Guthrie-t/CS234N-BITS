using System;
using System.Collections.Generic;
using System.Text;

namespace bitsEFClasses.Models
{
    public class SupplierVM
    {
        public int SupplierId { get; set; }
        public string Name { get; set; }
        public string StrLine1 { get; set; }
        public string StrLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
    }
}
