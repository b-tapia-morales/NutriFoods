using CsvHelper.Configuration;

namespace Utils.Averages;

public sealed class MealTypeRow
{
    public int MealTypeValue { get; set; }
    public double Energy { get; set; }
    public double Carbohydrates { get; set; }
    public double Proteins { get; set; }
    public double Lipids { get; set; }
}

public sealed class MealTypeMapping : ClassMap<MealTypeRow>
{
    public MealTypeMapping()
    {
        Map(p => p.MealTypeValue).Index(0);
        Map(p => p.Energy).Index(1);
        Map(p => p.Carbohydrates).Index(2);
        Map(p => p.Proteins).Index(3);
        Map(p => p.Lipids).Index(4);
    }
}