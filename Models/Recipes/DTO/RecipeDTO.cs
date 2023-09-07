using BigCatCookinAPI.Models.Recipes.DAO;

namespace BigCatCookinAPI.Models.Recipes.DTO;

public class RecipeDTO
{
    public string RecipeName { get; set; }
    public string RecipeDescription { get; set; }
    public int TotalServings { get; set; }


    public IList<IngredientDTO> Ingredients { get; set; }
    public IList<RecipeStepDTO> Steps { get; set; }
    public IList<ApplianceDTO> Appliances { get; set; }
    public IList<CookwareDTO> Cookware { get; set; }

    public RecipeDTO()
    {
        Ingredients = new List<IngredientDTO>();
        Steps = new List<RecipeStepDTO>();
        Appliances = new List<ApplianceDTO>();
        Cookware = new List<CookwareDTO>();
    }

    public RecipeDTO(string recipeName, 
        string recipeDescription, 
        int totalServings, 
        IList<IngredientDTO> ingredients, 
        IList<RecipeStepDTO> steps, 
        IList<ApplianceDTO> appliances, 
        IList<CookwareDTO> cookware)
    {
        RecipeName = recipeName;
        RecipeDescription = recipeDescription;
        TotalServings = totalServings;
        Ingredients = ingredients;
        Steps = steps;
        Appliances = appliances;
        Cookware = cookware;
    }

    public RecipeDTO(RecipeDAO recipe, 
        IList<IngredientDTO> ingredients, 
        IList<RecipeStepDTO> steps, 
        IList<ApplianceDTO> appliances, 
        IList<CookwareDTO> cookware)
    {
        RecipeName = recipe.RecipeName;
        RecipeDescription = recipe.RecipeDescription;
        TotalServings = recipe.TotalServings;
        Ingredients = ingredients;
        Steps = steps;
        Appliances = appliances;
        Cookware = cookware;
    }

    public RecipeDTO(RecipeDAO recipe)
    {
        RecipeName = recipe.RecipeName;
        RecipeDescription = recipe.RecipeDescription;
        TotalServings = recipe.TotalServings;
        Ingredients = new List<IngredientDTO>();
        Steps = new List<RecipeStepDTO>();
        Appliances = new List<ApplianceDTO>();
        Cookware = new List<CookwareDTO>();
    }
}
