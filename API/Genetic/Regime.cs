
using API.Dto;
using API.Recipes;
using Utils.Nutrition;

namespace API.Genetic;

public class Regime : IGeneticAlgorithm
{
    private readonly IRecipeRepository _repository;
    private readonly Random _r = new Random(Environment.TickCount);
    private static readonly ICollection<PossibleRegime> Solutions = new List<PossibleRegime>();
    private static readonly ICollection<PossibleRegime> Winners = new List<PossibleRegime>();
    private int _numberRecipe;


    public Regime(IRecipeRepository recipeRepository)
    {
        _repository = recipeRepository;
        _numberRecipe = 0;
    }
    public MealMenuDto GenerateSolution(int recipeAmount, double energyTotal, double carbohydratesPercentage, double lipidsPercentage, MealTypeDto mealType)
    {
        var recipes = GetUniverseRecipes();
        var macroNutrientsUser = EnergyDistribution.Calculate(energyTotal);
        GenerateInitialPopulation(recipeAmount,recipes);
        CalculatePopulationFitness(energyTotal,macroNutrientsUser.Carbohydrates,macroNutrientsUser.Proteins,macroNutrientsUser.Lipids);
        while (SolutionExist())
        {
            Selection();
            Crossover();
            Mutation(recipes);
            CalculatePopulationFitness(energyTotal,macroNutrientsUser.Carbohydrates,macroNutrientsUser.Proteins,macroNutrientsUser.Lipids);
        }
        
        return Solutions.First(pr => pr.Fitness == 8).Recipes;
    }
    public void CalculatePopulationFitness(double energyTotal, double userValueCarbohydrates,double userValueProteins, double userValurFats)
    {
        foreach (var possibleRegime in Solutions)
        {
            possibleRegime.MacroNutrientCalculation();
            possibleRegime.CalculateFitness(userValueCarbohydrates,userValueProteins,energyTotal,userValurFats);
        }
    }

    public void GenerateInitialPopulation(int cantRecipes, ICollection<MealMenuRecipeDto> totalRecipes)
    {
        _numberRecipe = cantRecipes;
        if (totalRecipes == null) throw new ArgumentNullException(nameof(totalRecipes));
        for (var i = 0; i < 6; i++)
        {
            var pr = new PossibleRegime(cantRecipes);
            for (var j = 0; j < cantRecipes; j++)
            {
                var rand = _r.Next(0, totalRecipes.Count);
                pr.AddRecipe(totalRecipes.ToList()[rand]);
            }
            Solutions.Add(pr);
        }
    }

    public void Selection()
    {
        var total = Solutions.Count / 2;
        var cantTournament = _r.Next(2, total);
        Winners.Clear();
        for (var i = 0; i < cantTournament;)
        {
            var fighter1 = _r.Next(0, Solutions.Count);
            var fighter2 = _r.Next(0, Solutions.Count);

            if (fighter1 != fighter2)
            {
                // veriricar si existe ya en los nuevos ganadores
                var win = Solutions.ToList()[fighter1].Fitness > Solutions.ToList()[fighter2].Fitness
                    ? Solutions.ToList()[fighter1]
                    : Solutions.ToList()[fighter2];
                var exist = Winners.All(s => s.Recipes.MenuRecipes.SequenceEqual(win.Recipes.MenuRecipes));
                if (!exist) Winners.Add(win);
                i++;
            }
        }
    }

    public void Crossover()
    {
        var probability = _r.NextDouble();
        if (probability <= 0.8)
        {
            for (var i = 0; i < 6;)
            {
                var father1 = Winners.ToList()[_r.Next(0, Winners.Count)];
                var father2 = Solutions.ToList()[_r.Next(0, Winners.Count)];

                var indexGenFather1 = _r.Next(0, father1.Recipes.MenuRecipes.ToList().Count);
                var indexGenFather2 = _r.Next(0, father2.Recipes.MenuRecipes.ToList().Count);

                var gen1 = father1.Recipes.MenuRecipes.ToList()[indexGenFather1];
                var gen2 = father2.Recipes.MenuRecipes.ToList()[indexGenFather2];

                if (gen1.Recipe.Id == gen2.Recipe.Id) continue;
                if (father1.Recipes.MenuRecipes.All(r => r.Recipe.Id != gen2.Recipe.Id) &&
                    father2.Recipes.MenuRecipes.All(r => r.Recipe.Id != gen1.Recipe.Id))
                {
                    //debe ir el intercambio
                    i+=2;
                }

            }
        }
    }

    public void Mutation(ICollection<MealMenuRecipeDto> totalRecipes)
    {
        if(_r.NextDouble() <= 0.4)
        {
            var numberMutation = _r.Next(1, Solutions.Count);
            for (var i = 0; i < numberMutation; i++)
            {
                var changePosition = _r.Next(0, Solutions.Count);
                var changeIndexRegime = _r.Next(0, _numberRecipe);
                var rateChangeRecipe = _r.Next(0, totalRecipes.Count);
                var changeRecipe = totalRecipes.ToList()[rateChangeRecipe];
                // con testin probar el cambio "Verificar ref o out"
                Solutions.ToList()[changePosition].Recipes.MenuRecipes.ToList()[changeIndexRegime] = changeRecipe;
            }
        }
        
    }
    

    public bool SolutionExist()
    {
        return Solutions.Any(r => r.Fitness != 8);
    }

    private ICollection<MealMenuRecipeDto> GetUniverseRecipes()
    {

        return _repository.FindAll().Result.Select(r => new MealMenuRecipeDto() {Recipe = r, Quantity = 1}).ToList();
    }
    
}