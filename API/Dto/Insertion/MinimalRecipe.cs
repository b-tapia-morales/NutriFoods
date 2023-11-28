// ReSharper disable ClassNeverInstantiated.Global

namespace API.Dto.Insertion;

public class MinimalRecipe
{
    public string Name { get; set; } = null!;
    public string Author { get; set; } = null!;
    public string Url { get; set; } = null!;
    public int? Portions { get; set; }
    public int? PreparationTime { get; set; }
    public string? Difficulty { get; set; }
    public List<string> MealTypes { get; set; } = null!;
    public List<string> DishTypes { get; set; } = null!;
    public List<MinimalMeasure> Measures { get; set; } = null!;
    public List<MinimalQuantity> Quantities { get; set; } = null!;
    public List<string> Steps { get; set; } = null!;
}

public class MinimalMeasure
{
    public string Name { get; set; } = null!;
    public string IngredientName { get; set; } = null!;
    public int IntegerPart { get; set; }
    public int Numerator { get; set; }
    public int Denominator { get; set; }
}

public class MinimalQuantity
{
    public string IngredientName { get; set; } = null!;
    public double Grams { get; set; }
}