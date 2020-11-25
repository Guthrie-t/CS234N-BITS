using System;
using System.Collections.Generic;

namespace TermProjectEFCore.Models
{
    public partial class BatchContainer
    {
        public int BatchId { get; set; }
        public int BrewContainerId { get; set; }
        public DateTime DateIn { get; set; }
        public DateTime? DateOut { get; set; }
        public double? Volume { get; set; }

        public virtual Batch Batch { get; set; }
        public virtual BrewContainer BrewContainer { get; set; }
    }
}
