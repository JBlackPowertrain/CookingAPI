using BigCatCookinAPI.Models.GPT.GPTResponses;
using BigCatCookinAPI.Models.GPT.GPTResponses.GPTRecipes;

namespace BigCatCookinAPI.Services.Interface;

public interface IChatGPTService
{
    public Task<string> AnswerFAQ(string genericQuestion);

    public Task<GPTDietResponse> GetDietPlans(string allergies, string dietaryIssues, string pallet);

    public Task<GPTRecipeResponse> GetRecipe(string allergies, 
        string dietaryRestrictions,
        string pallet,
        string availableAppliances,
        string availableUtensils,
        string ingredients,
        string previousDishes);
}
