using Domain.Enum;

namespace Domain.Models;

public class FoodConsumption
{
    public Guid Id { get; set; }

    public FoodGroups FoodGroup { get; set; } = null!;

    public Frequencies Frequency { get; set; } = null!;

    public Guid NutritionalAnamnesis { get; set; }

    public virtual NutritionalAnamnesis NutritionalAnamnesesNavigation { get; set; } = null!;
}
