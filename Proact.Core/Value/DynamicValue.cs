using Proact.Core.Tag.Context;

namespace Proact.Core.Value;

public static class DynamicValue
{
    public static RootValue<T> Create<T>(string id, T initialValue)
    {
        return new RootValue<T>(id, initialValue);
    }
    
    public static RootValue<T> CreateWithContext<T>(string id, Func<IRenderContext, T> initialValueCreator)
    {
        return new RootValue<T>(id, new RootValueOptions<T>
        {
            InitialValueCreator = initialValueCreator, 
        });
    }

    public static RootValue<string?> CreatePathParameter(string pathParameter)
    {
        return CreateWithContext(pathParameter, c => c.PathParameters.GetValueOrDefault(pathParameter));
    }

    public static RootValue<string?> CreateQueryParameter(string queryParameter)
    {
        return CreateWithContext(queryParameter,
            c => c.QueryParameters.TryGetValue(queryParameter, out var values) ? values[0] : null);
    }
    public static RootValue<List<string>> CreateQueryParameters(string queryParameter)
    {
        return CreateWithContext(queryParameter, c => c.QueryParameters.GetValueOrDefault(queryParameter, new List<string>()));
    }
}