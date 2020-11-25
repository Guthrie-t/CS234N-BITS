using System;
using System.Collections.Generic;

namespace TermProjectEFCore.Models
{
    public partial class Supplier
    {
        public Supplier()
        {
            IngredientInventoryAddition = new HashSet<IngredientInventoryAddition>();
            SupplierAddress = new HashSet<SupplierAddress>();
        }

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

        public virtual ICollection<IngredientInventoryAddition> IngredientInventoryAddition { get; set; }
        public virtual ICollection<SupplierAddress> SupplierAddress { get; set; }
    }
}
