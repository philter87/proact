using System.Text.Json;

namespace Proact.Core;

public static class Json
{
    private static readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public static string Parse<T>(T val) 
    {
        if (val == null)
        {
            return default;
        }
        
        if (typeof(T) == typeof(string))
        {
            return val.ToString()!;
        }
        if (typeof(T) == typeof(int))
        {
            return val.ToString()!;
        }
        if (typeof(T) == typeof(bool))
        {
            return val.ToString();
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
            return (T)(object)val;
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