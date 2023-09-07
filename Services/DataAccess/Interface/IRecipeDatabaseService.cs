using BigCatCookinAPI.Models.Recipes.DAO;
using BigCatCookinAPI.Models.Recipes.DTO;

namespace BigCatCookinAPI.Services.DataAccess.Interface;

public interface IRecipeDatabaseService
{
    public RecipeDAO GetRecipeFromName(string name);
    public RecipeDAO GetRecipeFromId(Guid id);

    public IList<IngredientDAO> GetRecipeIngredients(Guid id);
    public IList<RecipeStepDAO> GetRecipeSteps(Guid id);
    public IList<ApplianceDAO> GetRecipeAppliances(Guid id);
    public IList<CookwareDAO> GetRecipeCookware(Guid id);

    public bool InsertRecipe(RecipeDTO recipe);
}
