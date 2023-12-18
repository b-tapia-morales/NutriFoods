namespace Domain.Models;

public class EatingSymptom
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Observations { get; set; }

    public Guid NutritionalAnamnesisId { get; set; }

    public virtual NutritionalAnamnesis NutritionalAnamnesis { get; set; } = null!;
}
