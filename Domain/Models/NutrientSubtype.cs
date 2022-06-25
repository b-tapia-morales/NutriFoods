namespace Domain.Models;

public class NutrientSubtype
{
    public NutrientSubtype()
    {
        Nutrients = new HashSet<Nutrient>();
    }

    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public bool ProvidesEnergy { get; set; }
    public int TypeId { get; set; }

    public virtual NutrientType Type { get; set; } = null!;
    public virtual ICollection<Nutrient> Nutrients { get; set; }
}