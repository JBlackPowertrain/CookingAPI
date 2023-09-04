using BigCatCookinAPI.Models.Recipes.DAO;

namespace BigCatCookinAPI.Models.Recipes.DTO;

public class IngredientDTO
{
    public string IngredientName { get; set; }
    public string IngredientDescription { get; set; }
    public string Status { get; set; }
    public int Count { get; set; }
    public string Size { get; set; }
    public string SizeUnit { get; set; }
    public int ApproxCalories { get; set; }
    public float ApproximateProtein { get; set; }
    public float ApproximateFat { get; set; }
    public float ApproximateTotalCarbs { get; set; }
    public float ApproximateFiberCarbs { get; set; }
    public float ApproximateNetCarbs { get; set; }
    public float ApproximateSodium { get; set; }

    public IngredientDTO(string ingredientName, 
        string ingredientDescription, 
        string status, string size, string sizeUnit, 
        int approxCalories, float approximateProtein, 
        float approximateFat, float approximateTotalCarbs, 
        float approximateFiberCarbs, float approximateNetCarbs, 
        float approximateSodium)
    {
        IngredientName = ingredientName;
        IngredientDescription = ingredientDescription;
        Status = status;
        Size = size;
        SizeUnit = sizeUnit;
        ApproxCalories = approxCalories;
        ApproximateProtein = approximateProtein;
        ApproximateFat = approximateFat;
        ApproximateTotalCarbs = approximateTotalCarbs;
        ApproximateFiberCarbs = approximateFiberCarbs;
        ApproximateNetCarbs = approximateNetCarbs;
        ApproximateSodium = approximateSodium;
    }

    public IngredientDTO(IngredientDAO dao)
    {
        IngredientName = dao.IngredientName;
        IngredientDescription = dao.IngredientDescription;
        Status = dao.Status;
        Size = dao.Size;
        SizeUnit = dao.SizeUnit;
        ApproxCalories = dao.ApproxCalories;
        ApproximateProtein = dao.ApproximateProtein;
        ApproximateFat = dao.ApproximateFat;
        ApproximateTotalCarbs = dao.ApproximateTotalCarbs;
        ApproximateFiberCarbs = dao.ApproximateFiberCarbs;
        ApproximateNetCarbs = dao.ApproximateNetCarbs;
        ApproximateSodium = dao.ApproximateSodium;
    }

    public static IList<IngredientDTO> getIngredients(
        IList<IngredientDAO> ingredients)
    {
        List<IngredientDTO> _ingredients= new List<IngredientDTO>();
        foreach(IngredientDAO dao in ingredients)
        {
            _ingredients.Add(new IngredientDTO(dao));
        }
        return _ingredients;
    }
}
