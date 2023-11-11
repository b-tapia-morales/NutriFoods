using NutrientRetrieval.Food;

// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
// ReSharper disable ClassNeverInstantiated.Global

namespace NutrientRetrieval.FullRetrieval;

public class Food : IFood<FoodNutrient>
{
    public int FdcId { get; set; }
    public string Description { get; set; } = null!;
    public FoodNutrient[] FoodNutrients { get; set; } = null!;
    public FoodPortion[] FoodPortions { get; set; } = null!;
    public int NdbNumber { get; set; }

    public override string ToString()
    {
        return $"""
                {FdcId}
                {Description}
                {NdbNumber}
                {string.Join(Environment.NewLine, (IEnumerable<FoodPortion>)FoodPortions)}
                """;
    }
}

public class FoodNutrient : IFoodNutrient
{
    public Nutrient Nutrient { get; set; } = null!;
    public double Amount { get; set; }

    string IFoodNutrient.Number => Nutrient.Number;
    string IFoodNutrient.Name => Nutrient.Name;
    double IFoodNutrient.Amount => Amount;
    string IFoodNutrient.UnitName => Nutrient.UnitName;

    public override string ToString() =>
        $"""
         {Nutrient}
         {Amount}
         """;
}

public class Nutrient
{
    public int Id { get; set; }
    public string Number { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string UnitName { get; set; } = null!;

    public override string ToString() =>
        $"""
         {Id}
         {Number}
         {Name}
         {UnitName}
         """;
}

public class FoodPortion
{
    public double GramWeight { get; set; }
    public double Amount { get; set; }
    public string Modifier { get; set; } = null!;

    public override string ToString() =>
        $"""
         {Modifier}
         {Amount}
         {GramWeight}
         """;
}