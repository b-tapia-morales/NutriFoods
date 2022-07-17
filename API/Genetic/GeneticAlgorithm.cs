using API.Dto;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Genetic;

public class GeneticAlgorithm
{
    private readonly List<Solutions> _solutionCandidates;
    private readonly List<Solutions> _winners;
    private readonly List<Recipe> _population;
    private readonly int _recipesAmount;
    private readonly double _energyTarget;
    private readonly double _carbohydratesTarget;
    private readonly double _proteinsTarget;
    private readonly double _lipidsTarget;

    private const string ConnectionString =
        "Host=localhost;Database=nutrifoods_db;Username=nutrifoods_dev;Password=MVmYneLqe91$";

    public GeneticAlgorithm(int recipesAmount, double energyTarget, double carbohydratesTarget, double proteinsTarget,
        double lipidsTarget)
    {
        _solutionCandidates = new List<Solutions>(recipesAmount);
        _winners = new List<Solutions>(recipesAmount);
        _population = TotalRecipes();
        _recipesAmount = recipesAmount;
        _energyTarget = energyTarget;
        _carbohydratesTarget = carbohydratesTarget;
        _proteinsTarget = proteinsTarget;
        _lipidsTarget = lipidsTarget;
    }

    public static List<Recipe> TotalRecipes()
    {
        var options = new DbContextOptionsBuilder<NutrifoodsDbContext>()
            .UseNpgsql(ConnectionString,
                builder => builder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery))
            .Options;
        using var context = new NutrifoodsDbContext(options);
        var recipes = IncludeSubfields(context.Recipes).Where(e => e.Portions is > 0).ToList();
        return recipes;
    }

    public List<Solutions> GenerateMealPlan()
    {
        var seed = Environment.TickCount;
        var random = new Random(seed);
        GenerateSolutions();
        for (var i = 0; i < 1000; i++)
        {
            Tournament(random);
            Crossover(random);
            Mutation(random);
            ModifyFitness();
        }

        Solutions();
        SolutionsFinal();
        return _solutionCandidates;
    }

    private void GenerateSolutions()
    {
        var seed = Environment.TickCount;
        var random = new Random(seed);
        for (var i = 0; i < _recipesAmount; i++)
        {
            /* una solución consta de 3 platos por ejemplo un almuerzo
             se crea una solución de tamaño 3 [R1,R2,R3]
            */
            var s = new Solutions(6);
            for (var j = 0; j < 6; j++)
            {
                // numero random para elegir al azar una receta
                var rand = random.Next(0, _population.Count);
                s.AddGene(_population[rand]);
            }

            // termine de generar y lo ingresa a la lista de soluciones
            _solutionCandidates.Add(s);
        }
    }

    private void Tournament(Random r)
    {
        var tournaments = r.Next(2, _solutionCandidates.Count);
        _winners.Clear();
        for (var i = 0; i < tournaments; i++)
        {
            int firstCombatant, secondCombatant;
            do
            {
                firstCombatant = r.Next(0, _solutionCandidates.Count);
                secondCombatant = r.Next(0, _solutionCandidates.Count);
            } while (firstCombatant == secondCombatant);

            var winnerIndex = _solutionCandidates[firstCombatant].Fitness > _solutionCandidates[secondCombatant].Fitness
                ? firstCombatant
                : secondCombatant;
            _winners.Add(_solutionCandidates[winnerIndex]);
        }
    }

    private static Solutions Sons(Recipe r, int index, Solutions father)
    {
        var s = new Solutions(6);
        for (var i = 0; i < father.ListRecipe.Count; i++)
        {
            s.AddGene(index != i ? father.ListRecipe[i] : r);
        }

        return s;
    }

    private void Crossover(Random r)
    {
        var probability = r.NextDouble();
        var sons = new List<Solutions>();
        if (probability > 0.8) return;
        for (var i = 0; i < 6; i += 2)
        {
            // selecciona uno de la lista de ganadores
            var parent1 = _winners[r.Next(0, _winners.Count)];
            // selecciona uno al azar de los anteriores
            var parent2 = _solutionCandidates[r.Next(0, _solutionCandidates.Count)];

            //selecciona al azar una posiciones para cada padre
            var indexGen1 = r.Next(0, parent1.ListRecipe.Count);
            var indexGen2 = r.Next(0, parent2.ListRecipe.Count);

            var gen1 = parent1.ListRecipe[indexGen1];
            var gen2 = parent2.ListRecipe[indexGen2];


            // se produce el intercambio de genes
            if (gen1.Id == gen2.Id) continue;
            sons.Add(Sons(gen2, indexGen1, parent1));
            sons.Add(Sons(gen1, indexGen2, parent2));
        }

        _solutionCandidates.Clear();
        _solutionCandidates.AddRange(sons);
    }

    private void Mutation(Random r)
    {
        if (r.NextDouble() > 0.4) return;
        var mutationsAmount = r.Next(1, _solutionCandidates.Count);
        for (var i = 0; i < mutationsAmount; i++)
        {
            var index = r.Next(0, _solutionCandidates.Count);
            var indexAMutation = r.Next(0, _solutionCandidates[index].ListRecipe.Count);
            var indexNewRecipe = r.Next(0, _population.Count);
            _solutionCandidates[index] = NewSolution(index, indexAMutation, indexNewRecipe);
        }
    }

    private Solutions NewSolution(int index, int newIndex, int indexNewRecipe)
    {
        var s = new Solutions(6);
        for (var i = 0; i < _solutionCandidates[index].ListRecipe.Count; i++)
        {
            s.AddGene(i != newIndex ? _solutionCandidates[index].ListRecipe[i] : _population[indexNewRecipe]);
        }

        return s;
    }

    private static IEnumerable<Recipe> IncludeSubfields(IQueryable<Recipe> recipes)
    {
        return recipes
            .Include(e => e.RecipeNutrients)
            .ThenInclude(e => e.Nutrient)
            .Include(e => e.RecipeMeasures)
            .ThenInclude(e => e.IngredientMeasure)
            .ThenInclude(e => e.Ingredient)
            .ThenInclude(e => e.IngredientNutrients)
            .ThenInclude(e => e.Nutrient)
            .Include(e => e.RecipeQuantities)
            .ThenInclude(e => e.Ingredient)
            .ThenInclude(e => e.IngredientNutrients)
            .ThenInclude(e => e.Nutrient)
            .AsNoTracking();
    }

    private void ModifyFitness()
    {
        foreach (var solutions in _solutionCandidates)
        {
            solutions.CalculateFitness();
            solutions.EvaluateFitness(_carbohydratesTarget, _proteinsTarget, _energyTarget, _lipidsTarget);
        }
    }

    private void Solutions()
    {
        foreach (var solutions in _solutionCandidates)
        {
            foreach (var recipe in solutions.ListRecipe)
            {
                Console.Write(recipe.Id + " ");
            }

            Console.Write(" | " + solutions.Fitness + " | " + solutions.EnergyTotal);
            Console.WriteLine();
        }

        Console.WriteLine("-----------------------");
    }

    private void SolutionsFinal()
    {
        foreach (var solutions in _solutionCandidates)
        {
            solutions.Print();
        }

        Console.WriteLine();
    }

    private bool ExistWinners(Solutions s)
    {
        foreach (var solutions in _winners)
        {
            int i;
            for (i = 0; i < solutions.ListRecipe.Count; i++)
            {
                if (!solutions.ListRecipe[i].Name.Equals(s.ListRecipe[i].Name)) break;
            }

            if (i == solutions.ListRecipe.Count)
            {
                return true;
            }
        }

        return false;
    }
}