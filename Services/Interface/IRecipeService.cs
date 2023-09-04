using BigCatCookinAPI.Models.Recipes.DTO;

namespace BigCatCookinAPI.Services.Interface;

public interface IRecipeService
{
    public RecipeDTO GetRecipeFromName(string name);
    public RecipeDTO GetRecipeFromId(Guid id);

}
