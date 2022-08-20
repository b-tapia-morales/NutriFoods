namespace RecipeAndMesuris.Normalization;

public class ValuesIngredientMeasures
{
    public string Name { get; set; }
    public int Frecuency { get; set; }

    public ValuesIngredientMeasures(string name)
    {
        this.Name = name;
        Frecuency = 0;
    }

    public bool AddFrecuency()
    {
        Frecuency++;
        return true;
    }
}