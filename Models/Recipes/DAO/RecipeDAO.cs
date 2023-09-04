namespace BigCatCookinAPI.Models.Recipes.DAO;

public class RecipeDAO
{
    public Guid RecipeId { get; set; }
    public string RecipeName { get; set; }
    public string RecipeDescription { get; set; }

    public int TotalServings { get; set; }
}
