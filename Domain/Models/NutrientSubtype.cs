namespace Domain.Models;

public sealed class NutrientSubtype
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public bool ProvidesEnergy { get; set; }

    public int TypeId { get; set; }

    public ICollection<Nutrient> Nutrients { get; } = new List<Nutrient>();

    public NutrientType Type { get; set; } = null!;
}
