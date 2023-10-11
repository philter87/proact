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
        return new RootValue<string?>(queryParameter, new RootValueOptions<string?>()
        {
            InitialValueCreator = c => c.QueryParameters.TryGetValue(queryParameter, out var values) ? values[0] : null,
            IsQueryParameter = true,
        });
    }
    
    public static RootValue<string> CreateQueryParameter(string queryParameter, string defaultValue)
    {
        return new RootValue<string>(queryParameter, new RootValueOptions<string>()
        {
            InitialValueCreator = c => c.QueryParameters.TryGetValue(queryParameter, out var values) ? values[0] : defaultValue,
            IsQueryParameter = true,
        });
    }
    public static RootValue<List<string>> CreateQueryParameters(string queryParameter)
    {
        return new RootValue<List<string>>(queryParameter, new RootValueOptions<List<string>>()
        {
            InitialValueCreator = c => c.QueryParameters.GetValueOrDefault(queryParameter, new List<string>()),
            IsQueryParameter = true,
        });
    }
}