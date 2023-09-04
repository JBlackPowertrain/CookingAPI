namespace BigCatCookinAPI.Models.GPT.GPTResponses.GPTRecipes;

public class GPTRecipeResponse
{
    public string Name { get; set; }
    public string Description { get; set; }
    public IList<GPTRecipeSteps> StepsToCreate { get; set; }
    public IList<GPTRecipeCookware> CookwareRequired { get; set; }
    public IList<GPTRecipeAppliances> AppliancesRequired { get; set; }
    public int ServingCount { get; set; }


    public IList<GPTIngredient> Ingredients { get; set; }
}
