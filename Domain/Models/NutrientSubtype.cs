namespace Domain.Models;

public sealed class NutrientSubtype
{
    public NutrientSubtype() => Nutrients = new HashSet<Nutrient>();

    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public bool ProvidesEnergy { get; set; }
    public int TypeId { get; set; }

    public NutrientType Type { get; set; } = null!;
    public ICollection<Nutrient> Nutrients { get; set; }
}