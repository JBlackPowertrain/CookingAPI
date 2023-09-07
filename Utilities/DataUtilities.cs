using BigCatCookinAPI.Models.Recipes.DTO;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json.Linq;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace BigCatCookinAPI.Utilities;

public static class DataUtilities
{

    public struct BlackCatInsertDef
    {
        public string sp; 
        public Dictionary<string, object> parameters;
    }

    public static Dictionary<string, object> GetParamList(string key, object val)
    {
        Dictionary<string, object> dict = new Dictionary<string, object>();
        dict.Add(key, val);
        return dict;
    }

    public static Dictionary<string, object> GetParamList(IList<string> key, IList<object> val)
    {
        Dictionary<string, object> dict = new Dictionary<string, object>();
        for (int x = 0; x < key.Count; x++)
        {
            dict.Add(key[x], val[x]);
        }

        return dict;
    }

    public static Dictionary<string,object> GetParamListFromObj(object obj)
    {
        Dictionary<string, object> _params = 
            new Dictionary<string, object>();

        string[] properties = GetPropertiesFromObject(obj.GetType());
        foreach(string property in properties)
        {
            object value = obj.GetType().GetProperty(property).
                GetValue(obj, null);

            //Check to see if the property IS NOT NULL AND NOT DEFAULT
            //IF these are both true, add them to the dictionary. 
            //This should function should primarily be used for sprocs
            //So if a param is a default value (0, "", null, etc) it has 
            //the potential to fuck with indexing and we don't want that. 
            if (value != null && !value.Equals(obj.GetType().GetProperty(property).
                GetType().GetDefaultValue()))
            {
                if (!object.Equals(value, default(object)))
                {
                    _params.Add(property, value);
                }
            }
        }

        return _params;
    }

    public static string[] GetPropertiesFromObject(Type type)
    {
        BindingFlags bindingFlags = BindingFlags.Public |
                                    BindingFlags.Instance;

        List<string> properties = new List<string>();
        foreach (PropertyInfo info in type.GetProperties(bindingFlags))
        {
            properties.Add(info.Name);
        }
        return properties.ToArray();
    }
}
