namespace Domain.Models
{
    public class Recipe
    {
        public Recipe()
        {
            MealMenuRecipes = new HashSet<MealMenuRecipe>();
            RecipeDiets = new HashSet<RecipeDiet>();
            RecipeDishTypes = new HashSet<RecipeDishType>();
            RecipeMealTypes = new HashSet<RecipeMealType>();
            RecipeSections = new HashSet<RecipeSection>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Author { get; set; } = null!;
        public string? Url { get; set; }
        public int? Portions { get; set; }
        public int? PreparationTime { get; set; }

        public virtual ICollection<MealMenuRecipe> MealMenuRecipes { get; set; }
        public virtual ICollection<RecipeDiet> RecipeDiets { get; set; }
        public virtual ICollection<RecipeDishType> RecipeDishTypes { get; set; }
        public virtual ICollection<RecipeMealType> RecipeMealTypes { get; set; }
        public virtual ICollection<RecipeSection> RecipeSections { get; set; }
    }
}
