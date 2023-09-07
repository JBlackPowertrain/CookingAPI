namespace BigCatCookinAPI.Models.GPT.GPTResponses.GPTRecipes;

public class GPTIngredient
{
    public string Name { get; set; }
    public string Size { get; set; }
    public string SizeUnit { get; set; }
    public float Count { get; set; }
    public string Status { get; set; }
    public int ApproximateCaloricCount { get; set; }
    public float ApproximateProtein { get; set; }
    public float ApproximateFat { get; set; }
    public float ApproximateTotalCarbs { get; set; }
    public float ApproximateFiberCarbs { get; set; }
    public float ApproximateNetCarbs { get; set; }
    public float ApproximateSodium { get; set; }
}
