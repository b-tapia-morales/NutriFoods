namespace NutrientRetrieval.Model;

public class FoodNutrient
{
    public string Number { get; set; }
    public string Name { get; set; }
    public float Amount { get; set; }
    public string UnitName { get; set; }

    public override string ToString()
    {
        return $@"
{Number}
{Name}
{Amount}
{UnitName}";
    }
}