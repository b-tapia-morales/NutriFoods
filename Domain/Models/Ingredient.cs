using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Ingredient
    {
        public Ingredient()
        {
            IngredientQuantities = new HashSet<IngredientQuantity>();
            Measures = new HashSet<Measure>();
            UserAllergies = new HashSet<UserAllergy>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public bool IsAnimal { get; set; }
        public bool ContainsGluten { get; set; }
        public int TertiaryGroupId { get; set; }

        public virtual TertiaryGroup TertiaryGroup { get; set; } = null!;
        public virtual ICollection<IngredientQuantity> IngredientQuantities { get; set; }
        public virtual ICollection<Measure> Measures { get; set; }
        public virtual ICollection<UserAllergy> UserAllergies { get; set; }
    }
}
