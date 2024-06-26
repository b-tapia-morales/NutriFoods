﻿using Domain.Enum;

namespace Domain.Models;

public class Ingredient
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public List<string> Synonyms { get; set; } = null!;

    public bool IsAnimal { get; set; }

    public FoodGroups FoodGroup { get; set; } = null!;

    public virtual ICollection<IngredientMeasure> IngredientMeasures { get; set; } = null!;

    public virtual ICollection<RecipeQuantity> RecipeQuantities { get; set; } = null!;

    public virtual ICollection<NutritionalValue> NutritionalValues { get; set; } = null!;
}
