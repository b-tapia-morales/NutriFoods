namespace Domain.Models;

public class DishType
{
    public DishType()
    {
        RecipeDishTypes = new HashSet<RecipeDishType>();
    }

    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public virtual ICollection<RecipeDishType> RecipeDishTypes { get; set; }
}