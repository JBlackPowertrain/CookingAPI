using BigCatCookinAPI.Models.Recipes.DAO;

namespace BigCatCookinAPI.Models.Recipes.DTO;

public class ApplianceDTO
{
    public string ApplianceName { get; set; }
    public string Description { get; set; }

    public ApplianceDTO(string applianceName, string description)
    {
        ApplianceName = applianceName;
        Description = description;
    }

    public ApplianceDTO(ApplianceDAO dao)
    {
        ApplianceName = dao.ApplianceName;
        Description = dao.Description;
    }

    public static IList<ApplianceDTO> ApplianceDTOs(IList<ApplianceDAO> appliances) 
    { 
        List<ApplianceDTO> _appliances = new List<ApplianceDTO>();
        foreach(ApplianceDAO dao in appliances)
        {
            _appliances.Add(new ApplianceDTO(dao));
        }
        return _appliances;
    }
}
