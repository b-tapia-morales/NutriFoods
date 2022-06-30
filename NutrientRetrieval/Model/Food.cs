namespace NutrientRetrieval.Model;

public class Food
{
    public int FdcId { get; set; }
    public string Description { get; set; } = null!;
    public FoodNutrient[] FoodNutrients { get; set; } = null!;
    public FoodPortion[] FoodPortions { get; set; } = null!;
    public int NdbNumber { get; set; }
}

public class FoodNutrient
{
    public Nutrient Nutrient { get; set; } = null!;
    public string Type { get; set; } = null!;
    public int Id { get; set; }
    public float Amount { get; set; }
    public int DataPoints { get; set; }
}

public class Nutrient
{
    public int Id { get; set; }
    public string Number { get; set; } = null!;
    public string Name { get; set; } = null!;
    public int Rank { get; set; }
    public string UnitName { get; set; } = null!;
}

public class FoodPortion
{
    public int Id { get; set; }
    public float GramWeight { get; set; }
    public int SequenceNumber { get; set; }
    public int Amount { get; set; }
    public string Modifier { get; set; }
}