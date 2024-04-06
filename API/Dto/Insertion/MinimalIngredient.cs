// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedType.Global

namespace API.Dto.Insertion;

public class MinimalIngredient
{
    public int FoodDataCentralId { get; set; }
    public string Name { get; set; } = null!;
    public List<string> Synonyms { get; set; } = null!;
    public bool IsAnimal { get; set; }
    public string FoodGroup { get; set; } = null!;
    public List<MinimalIngredientMeasure> Measures { get; set; } = null!;
}

public class MinimalIngredientMeasure
{
    public string Name { get; set; } = null!;
    public double Grams { get; set; }
}