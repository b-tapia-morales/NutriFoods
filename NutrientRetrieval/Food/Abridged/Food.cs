// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable ClassNeverInstantiated.Global

namespace NutrientRetrieval.Food.Abridged;

public class Food : IFood<FoodNutrient>
{
    public int FdcId { get; set; }
    public string Description { get; set; } = null!;
    public FoodNutrient[] FoodNutrients { get; set; } = null!;

    public override string ToString() =>
        $"""
         {FdcId}
         {Description}
         {string.Join(Environment.NewLine, (IEnumerable<FoodNutrient>)FoodNutrients)}
         """;
}

public class FoodNutrient : IFoodNutrient
{
    public string Number { get; set; } = null!;
    public string Name { get; set; } = null!;
    public double Amount { get; set; }
    public string UnitName { get; set; } = null!;

    public override string ToString() =>
        $"""
         {Number}
         {Name}
         {Amount}
         {UnitName}
         """;
}