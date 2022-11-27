namespace Domain.Models;

public sealed class NutrientType
{
    public NutrientType() => NutrientSubtypes = new HashSet<NutrientSubtype>();

    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public ICollection<NutrientSubtype> NutrientSubtypes { get; set; }
}