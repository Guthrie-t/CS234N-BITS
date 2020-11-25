using System;
using System.Collections.Generic;

namespace bitsEFClasses.Models
{
    public partial class ContainerType
    {
        public ContainerType()
        {
            BrewContainer = new HashSet<BrewContainer>();
        }

        public int ContainerTypeId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<BrewContainer> BrewContainer { get; set; }
    }
}
