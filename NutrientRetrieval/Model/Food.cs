using System.Text.Json.Serialization;

namespace NutrientRetrieval.Model;

public class Food
{
    public int FdcId { get; set; }
    public string Description { get; set; } = null!;
    [JsonIgnore] public string PublicationDate { get; set; } = null!;
    public Foodnutrient[] FoodNutrients { get; set; } = null!;
    public Foodportion[] FoodPortions { get; set; } = null!;
    [JsonIgnore]
    public string DataType { get; set; } = null!;
    [JsonIgnore]
    public string FoodClass { get; set; } = null!;
    [JsonIgnore]
    public string ScientificName { get; set; } = null!;
    [JsonIgnore]
    public object[] FoodComponents { get; set; } = null!;
    [JsonIgnore]
    public object[] FoodAttributes { get; set; } = null!;
    [JsonIgnore]
    public Nutrientconversionfactor[] NutrientConversionFactors { get; set; } = null!;
    [JsonIgnore]
    public object[] InputFoods { get; set; } = null!;
    public int NdbNumber { get; set; }
    [JsonIgnore]
    public bool IsHistoricalReference { get; set; }
    [JsonIgnore]
    public Foodcategory FoodCategory { get; set; } = null!;
    
    public override string ToString()
    {
        return $@"
{FdcId}
{Description}
{NdbNumber}
{string.Join(Environment.NewLine, (IEnumerable<Foodportion>) FoodPortions)}";
    }
}

public class Foodcategory
{
    public int Id { get; set; }
    public string Code { get; set; } = null!;
    public string Description { get; set; } = null!;
}

public class Foodnutrient
{
    public Nutrient Nutrient { get; set; } = null!;
    [JsonIgnore]
    public string Type { get; set; } = null!;
    [JsonIgnore]
    public Foodnutrientderivation FoodNutrientDerivation { get; set; } = null!;
    [JsonIgnore]
    public int Id { get; set; }
    public float Amount { get; set; }
    [JsonIgnore]
    public int DataPoints { get; set; }
    [JsonIgnore]
    public float Max { get; set; }
    [JsonIgnore]
    public float Min { get; set; }
    
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
    public int Rank { get; set; }
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

public class Foodnutrientderivation
{
    public int Id { get; set; }
    public string Code { get; set; } = null!;
    public string Description { get; set; } = null!;
    public Foodnutrientsource FoodNutrientSource { get; set; } = null!;
}

public class Foodnutrientsource
{
    public int Id { get; set; }
    public string Code { get; set; } = null!;
    public string Description { get; set; } = null!;
}

public class Foodportion
{
    public int Id { get; set; }
    public double GramWeight { get; set; }
    public int SequenceNumber { get; set; }
    public double Amount { get; set; }
    public string Modifier { get; set; } = null!;
    [JsonIgnore]
    public Measureunit MeasureUnit { get; set; } = null!;
    
    public override string ToString()
    {
        return $@"
{Modifier}
{Amount}
{GramWeight}";
    }
}

public class Measureunit
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Abbreviation { get; set; } = null!;
}

public class Nutrientconversionfactor
{
    public int Id { get; set; }
    public float ProteinValue { get; set; }
    public float FatValue { get; set; }
    public float CarbohydrateValue { get; set; }
    public string Type { get; set; }
    public string Name { get; set; }
    public float Value { get; set; }
}