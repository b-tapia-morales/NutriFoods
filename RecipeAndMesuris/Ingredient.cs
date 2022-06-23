namespace RecipeAndMesuris;

public class Ingredient
{
    private string _nameIngredient;
    private int _frecuency;

    public Ingredient(string nameIngredient)
    {
        _nameIngredient = nameIngredient;
        _frecuency = 1;
    }

    public void SumFrecuency()
    {
        _frecuency++;
    }


    public string GetNameIngredient()
    {
        return _nameIngredient;
    }

    public int GetFrecuency()
    {
        return _frecuency;
    }
    
}