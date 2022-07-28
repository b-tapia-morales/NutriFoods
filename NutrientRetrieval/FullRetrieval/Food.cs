using NutrientRetrieval.Food;

namespace NutrientRetrieval.FullRetrieval;

public class Food : IFood
{
    public int FdcId { get; set; }
    public string Description { get; set; } = null!;
    public FoodNutrient[] FoodNutrients { get; set; } = null!;
    public FoodPortion[] FoodPortions { get; set; } = null!;
    public int NdbNumber { get; set; }

    int IFood.FdcId() => FdcId;

    string IFood.Description() => Description;

    public override string ToString()
    {
        return $@"
{FdcId}
{Description}
{NdbNumber}
{string.Join(Environment.NewLine, (IEnumerable<FoodPortion>) FoodPortions)}";
    }
}

public class FoodNutrient
{
    public Nutrient Nutrient { get; set; } = null!;
    public double Amount { get; set; }

    public override string ToString()
    {
        return $@"
{Nutrient}
{Amount}";
    }
}

public class Nutrient
{
    public int Id { get; set; }
    public string Number { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string UnitName { get; set; } = null!;

    public override string ToString()
    {
        return $@"
{Id}
{Number}
{Name}
{UnitName}";
    }
}

public class FoodPortion
{
    public double GramWeight { get; set; }
    public double Amount { get; set; }
    public string Modifier { get; set; } = null!;

    public override string ToString()
    {
        return $@"
{Modifier}
{Amount}
{GramWeight}";
    }
}