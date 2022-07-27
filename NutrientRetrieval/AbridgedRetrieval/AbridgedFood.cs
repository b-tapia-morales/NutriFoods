using NutrientRetrieval.Food;

namespace NutrientRetrieval.AbridgedRetrieval;

public class Food : IFood
{
    public int FdcId { get; set; }
    public string Description { get; set; } = null!;
    public FoodNutrient[] FoodNutrients { get; set; } = null!;

    int IFood.FdcId() => FdcId;

    string IFood.Description() => Description;

    public override string ToString()
    {
        return $@"
{FdcId}
{Description}
{string.Join(Environment.NewLine, (IEnumerable<FoodNutrient>) FoodNutrients)}";
    }
}

public class FoodNutrient
{
    public string Number { get; set; } = null!;
    public string Name { get; set; } = null!;
    public float Amount { get; set; }
    public string UnitName { get; set; } = null!;

    public override string ToString()
    {
        return $@"
{Number}
{Name}
{Amount}
{UnitName}";
    }
}