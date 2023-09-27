using System.Text.Json;
using System.Web;

namespace Proact.Core;

public static class Json
{
    private static readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public static string AsString<T>(T val) 
    {
        if (val == null)
        {
            return default;
        }
        
        if (val is string str)
        {
            return str;
        }

        if (val.GetType().IsPrimitive)
        {
            return val+"";
        }
        
        return JsonSerializer.Serialize(val, JsonSerializerOptions);
    }
    
    
    public static T? Parse<T>(string? val)
    {
        if (val == null)
        {
            return default;
        }
        
        if (typeof(T) == typeof(string))
        {
            return (T)(object)HttpUtility.HtmlEncode(val);
        }
        if (typeof(T) == typeof(int))
        {
            return (T)(object)int.Parse(val);
        }
        if (typeof(T) == typeof(bool))
        {
            return (T)(object)bool.Parse(val);
        }
        
        return JsonSerializer.Deserialize<T>(val, JsonSerializerOptions);
    }
}