using System;
using System.Collections.Generic;

namespace TermProjectEFCore.Models
{
    public partial class ContainerSize
    {
        public ContainerSize()
        {
            BrewContainer = new HashSet<BrewContainer>();
        }

        public int ContainerSizeId { get; set; }
        public string Name { get; set; }
        public double? MaxVolume { get; set; }
        public double? WorkingVolume { get; set; }

        public virtual ICollection<BrewContainer> BrewContainer { get; set; }
    }
}
