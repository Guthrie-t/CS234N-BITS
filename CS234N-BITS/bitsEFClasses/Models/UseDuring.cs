using System;
using System.Collections.Generic;

namespace bitsEFClasses.Models
{
    public partial class UseDuring
    {
        public UseDuring()
        {
            Adjunct = new HashSet<Adjunct>();
            RecipeIngredient = new HashSet<RecipeIngredient>();
        }

        public int UseDuringId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Adjunct> Adjunct { get; set; }
        public virtual ICollection<RecipeIngredient> RecipeIngredient { get; set; }
    }
}
