using BigCatCookinAPI.Models.Recipes.DAO;
using BigCatCookinAPI.Models.Recipes.DTO;
using BigCatCookinAPI.Services.DataAccess.Interface;
using BigCatCookinAPI.Services.Interface;

namespace BigCatCookinAPI.Services;

public class RecipeService : IRecipeService
{
    private readonly IRecipeDatabaseService _recipeDatabaseService;

    public RecipeService(IRecipeDatabaseService recipeDatabaseService)
    {
        _recipeDatabaseService = recipeDatabaseService;
    }

    public RecipeDTO GetRecipeFromName(string name)
    {
        return GetRecipe(
            _recipeDatabaseService.GetRecipeFromName(name));
    }

    public RecipeDTO GetRecipeFromId(Guid id)
    {
        return GetRecipe(_recipeDatabaseService.GetRecipeFromId(id));
    }

    public bool InsertRecipe(RecipeDTO recipeDTO)
    {
        return _recipeDatabaseService.InsertRecipe(recipeDTO);
    }

    protected RecipeDTO GetRecipe(RecipeDAO recipe)
    {
        RecipeDTO _recipe = new RecipeDTO(recipe);

        _recipe.Ingredients = IngredientDTO.getIngredients(
            _recipeDatabaseService.GetRecipeIngredients(recipe.RecipeId));

        _recipe.Steps = RecipeStepDTO.RecipeStepDTOs(
            _recipeDatabaseService.GetRecipeSteps(recipe.RecipeId));

        _recipe.Cookware = CookwareDTO.CookwareDTOs(
            _recipeDatabaseService.GetRecipeCookware(recipe.RecipeId));

        _recipe.Appliances = ApplianceDTO.ApplianceDTOs(
            _recipeDatabaseService.GetRecipeAppliances(recipe.RecipeId));

        return _recipe;
    }
}
