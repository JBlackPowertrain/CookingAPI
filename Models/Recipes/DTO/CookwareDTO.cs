using BigCatCookinAPI.Models.Recipes.DAO;

namespace BigCatCookinAPI.Models.Recipes.DTO;

public class CookwareDTO
{
    public string CookwareName { get; set; }
    public string Description { get; set; }

    public CookwareDTO(string cookwareName, string description)
    {
        CookwareName = cookwareName;
        Description = description;
    }

    public CookwareDTO(CookwareDAO dao)
    {
        CookwareName = dao.CookwareName;
        Description = dao.Description;
    }

    public static IList<CookwareDTO> CookwareDTOs(IList<CookwareDAO> cookware)
    {
        List<CookwareDTO> _cookware = new List<CookwareDTO>();
        foreach (CookwareDAO dao in cookware)
        {
            _cookware.Add(new CookwareDTO(dao));
        }
        return _cookware;
    }
}
