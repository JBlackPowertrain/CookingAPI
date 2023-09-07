using BigCatCookinAPI.Models.Recipes.DAO;
using BigCatCookinAPI.Models.Recipes.DTO;
using BigCatCookinAPI.Services.DataAccess.BigCatCookinDB;
using BigCatCookinAPI.Services.DataAccess.Interface;
using BigCatCookinAPI.Utilities;

namespace BigCatCookinAPI.Services.DataAccess;

public class RecipeDatabaseService : IRecipeDatabaseService
{
    private static string InsertIngredient = "InsertNewRecipeIngredient";
    private static string InsertAppliance = "InsertAppliance";
    private static string InsertCookware = "InsertCookware";
    private static string InsertRecipeAppliance = "InsertNewRecipeAppliance";
    private static string InsertRecipeCookware = "InsertNewRecipeCookware";
    private static string InsertRecipeStep = "InsertNewRecipeStep";

    private readonly IBigCatCookingDb _bigCatCookingDb;

    public RecipeDatabaseService(IBigCatCookingDb bigCatCookingDb)
    {
        _bigCatCookingDb = bigCatCookingDb;
    }

    public RecipeDAO GetRecipeFromName(string name)
    {
        var _params = DataUtilities.GetParamList("RecipeName", name);

        return _bigCatCookingDb.GetSingle<RecipeDAO>(
            _bigCatCookingDb.ConnectionStrings.RecipeDatabase,
            "SelectRecipe_By_Name",
            _params,
            typeof(RecipeDAO));
    }

    public RecipeDAO GetRecipeFromId(Guid id)
    {
        var _params = DataUtilities.GetParamList("RecipeId", id);

        return _bigCatCookingDb.GetSingle<RecipeDAO>(
            _bigCatCookingDb.ConnectionStrings.RecipeDatabase,
            "SelectRecipe_By_Id",
            _params,
            typeof(RecipeDAO));
    }

    public IList<IngredientDAO> GetRecipeIngredients(Guid id)
    {
        var _params = DataUtilities.GetParamList("RecipeId", id);

        return _bigCatCookingDb.Get<IngredientDAO>(
            _bigCatCookingDb.ConnectionStrings.RecipeDatabase,
            "SelectRecipeIngredients",
            _params,
            typeof(IngredientDAO));
    }

    public IList<RecipeStepDAO> GetRecipeSteps(Guid id)
    {
        var _params = DataUtilities.GetParamList("RecipeId", id);

        return _bigCatCookingDb.Get<RecipeStepDAO>(
            _bigCatCookingDb.ConnectionStrings.RecipeDatabase,
            "SelectRecipeSteps",
            _params,
            typeof(RecipeStepDAO));
    }

    public IList<ApplianceDAO> GetRecipeAppliances(Guid id)
    {
        var _params = DataUtilities.GetParamList("RecipeId", id);

        return _bigCatCookingDb.Get<ApplianceDAO>(
            _bigCatCookingDb.ConnectionStrings.RecipeDatabase,
            "SelectRecipeAppliances",
            _params,
            typeof(ApplianceDAO));
    }

    public IList<CookwareDAO> GetRecipeCookware(Guid id)
    {
        var _params = DataUtilities.GetParamList("RecipeId", id);

        return _bigCatCookingDb.Get<CookwareDAO>(
            _bigCatCookingDb.ConnectionStrings.RecipeDatabase,
            "SelectRecipeCookware",
            _params,
            typeof(CookwareDAO));
    }

    public bool InsertRecipe(RecipeDTO recipe)
    {
        try
        {
            Dictionary<string, object> _params = new Dictionary<string, object>();
            _params.Add("RecipeName", recipe.RecipeName);
            _params.Add("RecipeDescription", recipe.RecipeDescription);
            _params.Add("TotalServings", recipe.TotalServings);

            Guid _recipeId = _bigCatCookingDb.InsertGetId(_bigCatCookingDb.ConnectionStrings.RecipeDatabase,
                "InsertNewRecipe", _params);

            foreach (CookwareDTO cookware in recipe.Cookware)
            {
                Guid cookwareId = _bigCatCookingDb.InsertGetId(_bigCatCookingDb.ConnectionStrings.RecipeDatabase,
                    InsertCookware, DataUtilities.GetParamListFromObj(cookware));
                Dictionary<string, object> recCookwareParams =
                    DataUtilities.GetParamListFromObj(
                        new RecipeCookwareDAO()
                        {
                            CookwareId = cookwareId,
                            RecipeId = _recipeId
                        });
                _bigCatCookingDb.Insert(_bigCatCookingDb.ConnectionStrings.RecipeDatabase,
                    InsertRecipeCookware, recCookwareParams);
            }

            foreach (ApplianceDTO appliance in recipe.Appliances)
            {
                Guid cookwareId = _bigCatCookingDb.InsertGetId(_bigCatCookingDb.ConnectionStrings.RecipeDatabase,
                    InsertAppliance, DataUtilities.GetParamListFromObj(appliance));
                Dictionary<string, object> recCookwareParams =
                    DataUtilities.GetParamListFromObj(
                        new RecipeAppliancesDAO()
                        {
                            ApplianceId = cookwareId,
                            RecipeId = _recipeId
                        });
                _bigCatCookingDb.Insert(_bigCatCookingDb.ConnectionStrings.RecipeDatabase,
                    InsertRecipeAppliance, recCookwareParams);
            }
            
            foreach (IngredientDTO ingredient in recipe.Ingredients)
            {
                Dictionary<string, object> parameters = DataUtilities.GetParamListFromObj(ingredient);
                parameters.Add("RecipeId", _recipeId);
                _bigCatCookingDb.Insert(_bigCatCookingDb.ConnectionStrings.RecipeDatabase,
                    InsertIngredient, parameters);
            }

            foreach(RecipeStepDTO step in recipe.Steps)
            {
                Dictionary<string, object> parameters = DataUtilities.GetParamListFromObj(step);
                parameters.Add("RecipeId", _recipeId);
                _bigCatCookingDb.Insert(_bigCatCookingDb.ConnectionStrings.RecipeDatabase,
                    InsertRecipeStep, parameters);

            }
        }
        catch(Exception e)
        {
            return false; 
        }
        return true; 
    }
}
