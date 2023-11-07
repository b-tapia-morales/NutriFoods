namespace Domain.Models;

public sealed class EatingSymptom
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Observations { get; set; }

    public Guid NutritionalAnamnesis { get; set; }

    public NutritionalAnamnesis NutritionalAnamnesesNavigation { get; set; } = null!;
}
