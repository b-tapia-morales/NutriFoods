using API.Dto;

namespace API.Genetic;

public class Regime : IGeneticAlgorithm
{
    private readonly Random _random = new(Environment.TickCount);
    public IList<Chromosome> Solutions { get; } = new List<Chromosome>();
    public IList<Chromosome> Winners { get; } = new List<Chromosome>();

    public IList<MenuRecipeDto> GenerateTotalPopulation(IEnumerable<RecipeDto> recipes)
    {
        return recipes.Select(r => new MenuRecipeDto {Recipe = r, Portions = 1}).ToList();
    }

    public void CalculatePopulationFitness(double energy, double carbohydrates, double lipids, double proteins,
        double marginOfError)
    {
        foreach (var possibleRegime in Solutions)
        {
            possibleRegime.AggregateMacronutrients();
            possibleRegime.CalculateFitness(energy, carbohydrates, lipids, proteins, marginOfError);
        }
    }

    public void GenerateInitialPopulation(int recipesAmount, int solutionsAmount, IList<MenuRecipeDto> menus)
    {
        for (var i = 0; i < solutionsAmount; i++)
        {
            var listRecipe = new List<MenuRecipeDto>(recipesAmount);
            for (var j = 0; j < recipesAmount; j++)
            {
                var rand = _random.Next(0, menus.Count);
                listRecipe.Add(menus[rand]);
            }

            var pr = new Chromosome(listRecipe);
            Solutions.Add(pr);
        }
    }

    public void Selection()
    {
        var rangeMax = Solutions.Count / 2;
        var cantTournament = _random.Next(2, rangeMax);
        Winners.Clear();
        for (var i = 0; i < cantTournament;)
        {
            var fighter1 = _random.Next(0, Solutions.Count);
            var fighter2 = _random.Next(0, Solutions.Count);

            if (fighter1 == fighter2) continue;
            var win = Solutions[fighter1].Fitness > Solutions[fighter2].Fitness
                ? Solutions[fighter1]
                : Solutions[fighter2];
            var exist = Winners.Any(s => s.Recipes.MenuRecipes.SequenceEqual(win.Recipes.MenuRecipes));
            if (!exist) Winners.Add(win);
            i++;
        }
    }

    public void Crossover(int solutionsAmount)
    {
        var probability = _random.NextDouble();
        if (probability >= 0.8) return;
        var sonsNew = new List<Chromosome>();
        for (var i = 0; i < solutionsAmount;)
        {
            var father1 = Winners[_random.Next(0, Winners.Count)];
            var father2 = Solutions[_random.Next(0, Solutions.Count)];

            var indexGenFather1 = _random.Next(0, father1.Recipes.MenuRecipes.Count);
            var indexGenFather2 = _random.Next(0, father2.Recipes.MenuRecipes.Count);

            var gen1 = father1.Recipes.MenuRecipes[indexGenFather1];
            var gen2 = father2.Recipes.MenuRecipes[indexGenFather2];

            if (gen1.Recipe.Id == gen2.Recipe.Id) continue;
            if (father1.Recipes.MenuRecipes.Any(r => r.Recipe.Id == gen2.Recipe.Id) ||
                father2.Recipes.MenuRecipes.Any(r => r.Recipe.Id == gen1.Recipe.Id)) continue;

            var newChromosome1 = NewChromosome(father1, gen2, indexGenFather1);
            var newChromosome2 = NewChromosome(father2, gen1, indexGenFather2);

            sonsNew.Add(newChromosome1);
            sonsNew.Add(newChromosome2);
            i += 2;
        }

        Solutions.Clear();
        foreach (var newSon in sonsNew)
        {
            Solutions.Add(newSon);
        }
    }

    public void Mutation(IList<MenuRecipeDto> menus, int recipesAmount, int solutionsAmount)
    {
        if (_random.NextDouble() > 0.4) return;
        var numberMutation = _random.Next(1, Solutions.Count);
        for (var i = 0; i < numberMutation; i++)
        {
            var changePosition = _random.Next(0, solutionsAmount);
            var changeIndexRegime = _random.Next(0, recipesAmount);
            var rateChangeRecipe = _random.Next(0, menus.Count);
            var changeRecipe = menus[rateChangeRecipe];
            Solutions[changePosition] = NewMutation(changeIndexRegime, changeRecipe, changePosition);
        }
    }

    public bool SolutionExists()
    {
        return Solutions.Any(r => r.Fitness == 8);
    }

    public void ShowPopulation()
    {
        Console.WriteLine();
        foreach (var solution in Solutions)
        {
            solution.ShowPhenotypes();
        }

        Console.WriteLine();
    }

    private Chromosome NewMutation(int changeIndexRegime, MenuRecipeDto mealMenuRecipeDto, int changePosition)
    {
        var newRecipes = Solutions[changePosition].Recipes.MenuRecipes
            .Select((t, i) => i != changeIndexRegime ? t : mealMenuRecipeDto).ToList();

        var pr = new Chromosome(newRecipes);
        return pr;
    }

    private static Chromosome NewChromosome(Chromosome father, MenuRecipeDto gen, int indexFather)
    {
        var newChromosomal = father.Recipes.MenuRecipes.Select((t, i) => i == indexFather ? gen : t).ToList();
        var pr = new Chromosome(newChromosomal);

        return pr;
    }
}