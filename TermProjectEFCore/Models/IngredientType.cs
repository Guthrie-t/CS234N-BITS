using System;
using System.Collections.Generic;

namespace TermProjectEFCore.Models
{
    public partial class IngredientType
    {
        public IngredientType()
        {
            Ingredient = new HashSet<Ingredient>();
        }

        public int IngredientTypeId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Ingredient> Ingredient { get; set; }
    }
}
