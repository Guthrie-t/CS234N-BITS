using System;
using System.Collections.Generic;

namespace bitsEFClasses.Models
{
    public partial class Ingredient
    {
        public Ingredient()
        {
            IngredientInventoryAddition = new HashSet<IngredientInventoryAddition>();
            IngredientInventorySubtraction = new HashSet<IngredientInventorySubtraction>();
            IngredientSubstituteIngredient = new HashSet<IngredientSubstitute>();
            IngredientSubstituteSubstituteIngredient = new HashSet<IngredientSubstitute>();
            IngredientUsedIn = new HashSet<IngredientUsedIn>();
            RecipeIngredient = new HashSet<RecipeIngredient>();
        }

        public int IngredientId { get; set; }
        public string Name { get; set; }
        public int? Version { get; set; }
        public int IngredientTypeId { get; set; }
        public double OnHandQuantity { get; set; }
        public int UnitTypeId { get; set; }
        public decimal UnitCost { get; set; }
        public double ReorderPoint { get; set; }
        public string Notes { get; set; }

        public virtual IngredientType IngredientType { get; set; }
        public virtual UnitType UnitType { get; set; }
        public virtual Adjunct Adjunct { get; set; }
        public virtual Fermentable Fermentable { get; set; }
        public virtual Hop Hop { get; set; }
        public virtual Yeast Yeast { get; set; }
        public virtual ICollection<IngredientInventoryAddition> IngredientInventoryAddition { get; set; }
        public virtual ICollection<IngredientInventorySubtraction> IngredientInventorySubtraction { get; set; }
        public virtual ICollection<IngredientSubstitute> IngredientSubstituteIngredient { get; set; }
        public virtual ICollection<IngredientSubstitute> IngredientSubstituteSubstituteIngredient { get; set; }
        public virtual ICollection<IngredientUsedIn> IngredientUsedIn { get; set; }
        public virtual ICollection<RecipeIngredient> RecipeIngredient { get; set; }
    }
}
