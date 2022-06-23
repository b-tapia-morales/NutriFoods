namespace RecipeAndMesuris;

public class MissingIngredient
{
    private List<Ingredient> _missingIngredient;


    public MissingIngredient()
    {
        _missingIngredient = new List<Ingredient>();
    }

    public bool ExistIngredient(string nameIngredient)
    {
        foreach (var ingre in _missingIngredient)
        {
            if (ingre.GetNameIngredient().Equals(nameIngredient))
            {
                ingre.SumFrecuency();
                return true;
            }
        }

        Ingredient i = new Ingredient(nameIngredient);
        _missingIngredient.Add(i);
        return false;
    }

    public void Write()
    {
        List<Ingredient> missingIngredientOrden = _missingIngredient.OrderBy(x => x.GetFrecuency()).ToList();
        StreamWriter ingredientMissing = new StreamWriter("C:/Users/Eduardo/RiderProjects/NutriFoods-Latest/RecipeAndMesuris/Recipe_insert/Ingredient/measures/IngreMissing.csv");
        foreach (var ingre in missingIngredientOrden)
        {
            ingredientMissing.WriteLine(ingre.GetNameIngredient());
        }
        ingredientMissing.Close();
    }
}