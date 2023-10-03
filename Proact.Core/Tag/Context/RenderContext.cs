﻿namespace Proact.Core.Tag.Context;

public class RenderContext : IRenderContext
{
    public IServiceProvider ServiceProvider { get; set; }
    public string CurrentUrlPath { get; set; }
    public string CurrentUrlPattern { get; set; }
    public Dictionary<string, List<string>> QueryParameters { get; set; }
    public Dictionary<string, string> PathParameters { get; set; }
    public Dictionary<string, ValueChangeCommand> ValueChanges { get; set; }
    public List<ValueChangeCommand> ServerValueChanges { get; set; } = new();

    public RenderContext(IServiceProvider serviceProvider, string currentUrlPath, Dictionary<string, ValueChangeCommand>? values = null)
    {
        ServiceProvider = serviceProvider;
        CurrentUrlPath = currentUrlPath;
        CurrentUrlPattern = currentUrlPath;
        PathParameters = new Dictionary<string, string>();
        QueryParameters = RenderContextUtils.GetQueryParameters(currentUrlPath);
        ValueChanges = values ?? new Dictionary<string, ValueChangeCommand>();
    }

    public S? GetService<S>() where S: class
    {
        return (S?) ServiceProvider.GetService(typeof(S));
    }

    public void TriggerValueChange<T>(RootValue<T> value, T newValue)
    {
        ServerValueChanges.Add(new ValueChangeCommand(value.Id, Json.AsString(newValue)));
    }
}