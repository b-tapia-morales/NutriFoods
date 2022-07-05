namespace NutrientRetrieval.AbridgedModel;

public class Food
{
    public int FdcId { get; set; }
    public string Description { get; set; } = null!;
    public FoodNutrient[] FoodNutrients { get; set; } = null!;

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