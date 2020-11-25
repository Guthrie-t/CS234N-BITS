using System;
using System.Collections.Generic;

namespace bitsEFClasses.Models
{
    public partial class AdjunctType
    {
        public AdjunctType()
        {
            Adjunct = new HashSet<Adjunct>();
        }

        public int AdjunctTypeId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Adjunct> Adjunct { get; set; }
    }
}
