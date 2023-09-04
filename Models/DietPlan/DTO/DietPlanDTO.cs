namespace BigCatCookinAPI.Models.DietPlan.DTO;

public class DietPlanDTO
{
    public Guid DietPlanId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Benefits { get; set; }
    public string Drawbacks { get; set; }
}
