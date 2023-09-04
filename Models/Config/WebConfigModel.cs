namespace BigCatCookinAPI.Models.Config;

public class WebConfigModel
{
    public string GPTApiKey { get; private set; }
    public string MapsAPIKey { get; private set; }

    public WebConfigModel(string gPTApiKey, string mapsAPIKey)
    {
        GPTApiKey = gPTApiKey;
        MapsAPIKey = mapsAPIKey;
    }
}
