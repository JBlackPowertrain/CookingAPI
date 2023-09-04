namespace BigCatCookinAPI.Models.Recipes.DAO;

public class IngredientDAO
{
    public Guid IngredientId { get; set; }
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
}
