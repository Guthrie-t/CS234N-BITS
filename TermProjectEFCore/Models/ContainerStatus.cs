using System;
using System.Collections.Generic;

namespace TermProjectEFCore.Models
{
    public partial class ContainerStatus
    {
        public ContainerStatus()
        {
            BrewContainer = new HashSet<BrewContainer>();
        }

        public int ContainerStatusId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<BrewContainer> BrewContainer { get; set; }
    }
}
