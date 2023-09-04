using BigCatCookinAPI.Models.Recipes.DAO;
using BigCatCookinAPI.Models.Recipes.DTO;
using BigCatCookinAPI.Services.DataAccess.BigCatCookinDB;
using BigCatCookinAPI.Services.DataAccess.Interface;

namespace BigCatCookinAPI.Services.DataAccess;

public class RecipeDatabaseService : IRecipeDatabaseService
{

    private readonly IBigCatCookingDb _bigCatCookingDb;

    public RecipeDatabaseService(IBigCatCookingDb bigCatCookingDb)
    {
        _bigCatCookingDb = bigCatCookingDb;
    }

    public RecipeDAO GetRecipeFromName(string name)
    {
        var _params = GetParamList("RecipeName", name);

        return _bigCatCookingDb.GetSingle<RecipeDAO>(
            _bigCatCookingDb.ConnectionStrings.RecipeDatabase,
            "SelectRecipe_By_Name",
            _params,
            typeof(RecipeDAO));
    }

    public RecipeDAO GetRecipeFromId(Guid id)
    {
        var _params = GetParamList("RecipeId", id);

        return _bigCatCookingDb.GetSingle<RecipeDAO>(
            _bigCatCookingDb.ConnectionStrings.RecipeDatabase,
            "SelectRecipe_By_Id",
            _params,
            typeof(RecipeDAO));
    }

    public IList<IngredientDAO> GetRecipeIngredients(Guid id)
    {
        var _params = GetParamList("RecipeId", id);

        return _bigCatCookingDb.Get<IngredientDAO>(
            _bigCatCookingDb.ConnectionStrings.RecipeDatabase,
            "SelectRecipeIngredients",
            _params,
            typeof(IngredientDAO));
    }

    public IList<RecipeStepDAO> GetRecipeSteps(Guid id)
    {
        var _params = GetParamList("RecipeId", id);

        return _bigCatCookingDb.Get<RecipeStepDAO>(
            _bigCatCookingDb.ConnectionStrings.RecipeDatabase,
            "SelectRecipeSteps",
            _params,
            typeof(RecipeStepDAO));
    }

    public IList<ApplianceDAO> GetRecipeAppliances(Guid id)
    {
        var _params = GetParamList("RecipeId", id);

        return _bigCatCookingDb.Get<ApplianceDAO>(
            _bigCatCookingDb.ConnectionStrings.RecipeDatabase,
            "SelectRecipeAppliances",
            _params,
            typeof(ApplianceDAO));
    }

    public IList<CookwareDAO> GetRecipeCookware(Guid id)
    {
        var _params = GetParamList("RecipeId", id);

        return _bigCatCookingDb.Get<CookwareDAO>(
            _bigCatCookingDb.ConnectionStrings.RecipeDatabase,
            "SelectRecipeCookware",
            _params,
            typeof(CookwareDAO));
    }

    public bool InsertRecipe(RecipeDTO recipe)
    {

    }

    protected List<KeyValuePair<string, object>> GetParamList(string key, object val)
    {
        return new List<KeyValuePair<string, object>>()
        {
            (new KeyValuePair<string, object>(key, val))
        };
    }

}
