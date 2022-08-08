namespace RecipeAndMesuris.Normalization;

public class ValuesIngredientMeasures
{
    public string name { get; set; }
    public int frecuency { get; set; }

    public ValuesIngredientMeasures(string name)
    {
        this.name = name;
        frecuency = 0;
    }

    public bool AddFrecuency()
    {
        frecuency++;
        return true;
    }
}