using System;
using System.Collections.Generic;

namespace bitsEFClasses.Models
{
    public partial class ProductContainerSize
    {
        public ProductContainerSize()
        {
            InventoryTransaction = new HashSet<InventoryTransaction>();
            Product = new HashSet<Product>();
        }

        public int ContainerSizeId { get; set; }
        public string Name { get; set; }
        public double Volume { get; set; }
        public int ItemQuantity { get; set; }

        public virtual ProductContainerInventory ProductContainerInventory { get; set; }
        public virtual ICollection<InventoryTransaction> InventoryTransaction { get; set; }
        public virtual ICollection<Product> Product { get; set; }
    }
}
