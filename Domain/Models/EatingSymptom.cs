namespace Domain.Models;

public class EatingSymptom
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Observations { get; set; }

    public Guid NutritionalAnamnesis { get; set; }

    public virtual NutritionalAnamnesis NutritionalAnamnesesNavigation { get; set; } = null!;
}
