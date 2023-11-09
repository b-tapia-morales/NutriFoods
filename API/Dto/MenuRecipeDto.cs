using API.Dto.Abridged;

namespace API.Dto;

public class MenuRecipeDto
{
    public RecipeAbridged Recipe { get; set; } = null!;
    public int Portions { get; set; }
}