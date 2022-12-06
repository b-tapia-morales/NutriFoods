namespace Domain.Models;

public sealed class NutrientType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public ICollection<NutrientSubtype> NutrientSubtypes { get; } = new List<NutrientSubtype>();
}
