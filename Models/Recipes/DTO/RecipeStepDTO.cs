using BigCatCookinAPI.Models.Recipes.DAO;

namespace BigCatCookinAPI.Models.Recipes.DTO;

public class RecipeStepDTO
{
    public string RecipeStep { get; set; }
    public int RecipeStepNumber { get; set; }

    public RecipeStepDTO(string recipeStep, int recipeStepNumber)
    {
        RecipeStep = recipeStep;
        RecipeStepNumber = recipeStepNumber;
    }

    public RecipeStepDTO(RecipeStepDAO dao)
    {
        RecipeStep = dao.RecipeStep;
        RecipeStepNumber = dao.RecipeStepNumber;
    }

    public static IList<RecipeStepDTO> RecipeStepDTOs(IList<RecipeStepDAO> recipeSteps)
    {
        List<RecipeStepDTO> _recipeSteps = new List<RecipeStepDTO>();
        foreach(RecipeStepDAO dao in recipeSteps)
        {
            _recipeSteps.Add(new RecipeStepDTO(dao));
        }
        return _recipeSteps;
    }
}
