using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace BigCatCookinAPI.Utilities;

public class LowercaseContractResolver : DefaultContractResolver
{
    protected override string ResolvePropertyName(string propertyName)
    {
        return propertyName.ToLower();
    }

    public static JsonSerializerSettings GetLowercaseResolver()
    {
        JsonSerializerSettings settings = new JsonSerializerSettings();
        settings.ContractResolver = new LowercaseContractResolver();
        return settings; 
    }
}
