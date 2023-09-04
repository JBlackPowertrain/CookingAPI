namespace BigCatCookinAPI.Models.User.DAO;

public class UserDAO
{
    public Guid UserId { get; set; }
    public string UserName { get; set; }
    public string UserEmail { get; set; }
    public IList<Guid> GivenRecipeIds { get; set; }
    public IList<Guid> IgnoredRecipeIds { get; set; }
    public IList<Guid> DislikedRecipeIds { get; set; }
    public IList<Guid> IgnoredRecipes { get; set; }
    public IList<Guid> AvailableAppliances { get; set; }
    public IList<Guid> AvailableCookware { get; set; }
    public IList<Guid> Allergies { get; set; }
    public IList<Guid> DietaryRestrictions { get; set; }

    public IList<Guid> FavoriteIngredients { get; set; }
    public IList<Guid> DislikedIngredients { get; set; }

    public IList<Guid> UserFriends { get; set; }
}
