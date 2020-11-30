using bitsEFClasses.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace bitsEFClasses.ViewModels
{
    class SupplierAndAddress
    {
        public int SupplierId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string ContactFirstName { get; set; }
        public string ContactLastName { get; set; }
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }
        public string Note { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }
    }
}
