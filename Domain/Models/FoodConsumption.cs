using Domain.Enum;

namespace Domain.Models;

public class FoodConsumption
{
    public Guid Id { get; set; }

    public FoodGroups FoodGroup { get; set; } = null!;

    public Frequencies Frequency { get; set; } = null!;

    public Guid NutritionalAnamnesisId { get; set; }

    public virtual NutritionalAnamnesis NutritionalAnamnesis { get; set; } = null!;
}
