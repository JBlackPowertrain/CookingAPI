using BigCatCookinAPI.Models.GPT.GPTResponses.GPTRecipes;
using BigCatCookinAPI.Models.Recipes.DTO;

namespace BigCatCookinAPI.Models.GPT;

public static class GPTUtilities
{
    public static RecipeDTO GPTRecipeToDTO(GPTRecipeResponse recipe)
    {
        RecipeDTO dto = new RecipeDTO(); 
        dto.RecipeDescription = recipe.Description;
        dto.RecipeName = recipe.Name;
        dto.TotalServings = recipe.ServingCount; 

        foreach(GPTIngredient gptIngredient in recipe.Ingredients)
        {
            IngredientDTO ingredient = new IngredientDTO(gptIngredient.Name,
                gptIngredient.Count, gptIngredient.Status, gptIngredient.Size, gptIngredient.SizeUnit,
                gptIngredient.ApproximateCaloricCount, gptIngredient.ApproximateProtein,
                gptIngredient.ApproximateFat, gptIngredient.ApproximateTotalCarbs,
                gptIngredient.ApproximateFiberCarbs, gptIngredient.ApproximateNetCarbs,
                gptIngredient.ApproximateSodium);
            dto.Ingredients.Add(ingredient);
        }

        foreach(GPTRecipeCookware gptCookware in recipe.CookwareRequired)
        {
            CookwareDTO cookware = new CookwareDTO(gptCookware.Name,
                gptCookware.Description);
            dto.Cookware.Add(cookware);
        }

        foreach(GPTRecipeAppliances gptAppliance in recipe.AppliancesRequired)
        {
            ApplianceDTO appliance = new ApplianceDTO(gptAppliance.Name,
                gptAppliance.Description);
            dto.Appliances.Add(appliance);
        }

        foreach(GPTRecipeSteps gptStep in recipe.StepsToCreate)
        {
            RecipeStepDTO step = new RecipeStepDTO(gptStep.Step,
                gptStep.StepNumber);
            dto.Steps.Add(step);
        }

        return dto; 
    }
}
