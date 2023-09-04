using BigCatCookinAPI.Models.GPT.GPTResponses.GPTRecipes;
using BigCatCookinAPI.Services.Interface;
using BigCatCookinAPI.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace BigCatCookinAPI.Controllers;

public class RecipiesController : Controller
{
    private readonly IChatGPTService _chatGPTService;
    private readonly IRecipeService _recipeService; 
    private readonly ILogger<RecipiesController> _logger;

    public RecipiesController(ILogger<RecipiesController> logger, 
        IChatGPTService chatGPTService,
        IRecipeService recipeService)
    {
        _chatGPTService = chatGPTService;
        _recipeService = recipeService;
        _logger = logger;
    }

    //TODO: Change to Post with appropriate body/model
    [HttpGet("NewRecipe/{dietRestrictions}/{allergies}/{palletType}/{appliances}/{cookware}/{ingredients}/{previousDishes}")]
    public async Task<IActionResult> RequestRecipe(string dietRestrictions,
        string allergies,
        string palletType,
        string appliances,
        string cookware,
        string ingredients,
        string previousDishes)
    {
        dietRestrictions = Base64Utilities.Base64Decode(dietRestrictions).Trim();
        allergies = Base64Utilities.Base64Decode(allergies).Trim();
        palletType = Base64Utilities.Base64Decode(palletType).Trim();
        appliances = Base64Utilities.Base64Decode(appliances).Trim();
        cookware = Base64Utilities.Base64Decode(cookware).Trim();
        ingredients = Base64Utilities.Base64Decode(ingredients).Trim();
        previousDishes = Base64Utilities.Base64Decode(previousDishes).Trim();

        GPTRecipeResponse resp = await _chatGPTService.GetRecipe(dietRestrictions,
                allergies,
                palletType,
                appliances,
                cookware,
                ingredients,
                previousDishes);

        return StatusCode(StatusCodes.Status200OK, resp);
    }

    [HttpGet("GetRecipeByName/{recipeName}")]
    public async Task<IActionResult> GetPreviousRecipe(string recipeName)
    {
        return StatusCode(StatusCodes.Status200OK, 
            _recipeService.GetRecipeFromName(recipeName));
    }

    [HttpGet("GetRecipeById/{id}")]
    public async Task<IActionResult> GetPreviousRecipe(Guid id)
    {
        return StatusCode(StatusCodes.Status200OK,
            _recipeService.GetRecipeFromId(id)); 
    }
}
