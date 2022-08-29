using API.Dto;

namespace API.Genetic;

public class PossibleRegime
{
    private MealMenuDto Recipes { get; }
    private const double Percent = 0.1;

    public PossibleRegime(int numberRecipes)
    {
        Recipes = new MealMenuDto
        {
            EnergyTotal = 0,
            CarbohydratesTotal = 0,
            LipidsTotal = 0,
            ProteinsTotal = 0,
            MenuRecipes = new List<MealMenuRecipeDto>(numberRecipes)
        };
    }

    public void AddRecipe(RecipeDto r)
    {
        Recipes.MenuRecipes.ToList().Add(new MealMenuRecipeDto()
        {
            Recipe = r,
            Quantity = 1
        });
    }

    public void FitnessTotal()
    {
        double carbohydrates = 0;
        double lipids = 0;
        double proteins = 0;
        double energy = 0;
        foreach (var macroNutrients in Recipes.MenuRecipes.SelectMany(r => r.Recipe.RecipeNutrients.ToList()))
        {
            switch (macroNutrients.Nutrient.Id)
            {
                case 1:
                    energy += macroNutrients.Quantity;
                    break;
                case 2:
                    carbohydrates += macroNutrients.Quantity;
                    break;
                case 3:
                    proteins += macroNutrients.Quantity;
                    break;
                case 4:
                    lipids += macroNutrients.Quantity;
                    break;
            }
        }

        Recipes.CarbohydratesTotal = carbohydrates;
        Recipes.EnergyTotal = energy;
        Recipes.LipidsTotal = lipids;
        Recipes.ProteinsTotal = proteins;
    }

    public int Fitness(double userValueCarbohydrates, double userValueProteins, double userValueKilocalories, double userValueFats)
    {
        return FitnessResult(userValueKilocalories,Recipes.EnergyTotal) +
               FitnessResult(userValueProteins,Recipes.ProteinsTotal)+
               FitnessResult(userValueFats,Recipes.LipidsTotal)+
               FitnessResult(userValueCarbohydrates,Recipes.CarbohydratesTotal);
    }
    
    private static int FitnessResult(double userValue, double cantMicroNutrients )
    {
        if (userValue * (1 - Percent/2) <= cantMicroNutrients && userValue * (1 + (Percent/2)) >= cantMicroNutrients)
        {
            return 2;
        }

        if ((userValue * (1 - Percent) <= cantMicroNutrients && userValue * (1 - Percent / 2) > cantMicroNutrients) ||
            (userValue * (1 + Percent / 2) < cantMicroNutrients && userValue * (1 + Percent) >= cantMicroNutrients))
        {
            return 0;
        }

        if (userValue * (1 - Percent) > cantMicroNutrients ||
            userValue * (1 + Percent) < cantMicroNutrients)
        {
            return -2;
        }

        return 0;
    }
}