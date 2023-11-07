using Domain.Enum;

namespace Domain.Models;

public sealed class FoodConsumption
{
    public Guid Id { get; set; }

    public FoodGroup FoodGroup { get; set; } = null!;

    public Frequency Frequency { get; set; } = null!;

    public Guid NutritionalAnamnesis { get; set; }

    public NutritionalAnamnesis NutritionalAnamnesesNavigation { get; set; } = null!;
}
