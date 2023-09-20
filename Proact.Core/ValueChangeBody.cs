namespace Proact.Core;
public class DynamicHtmlResult
{
    public string? InitialValue { get; set; }
    public object? Value { get; set; }
    public Dictionary<string, string> IdToHtml { get; set; } = new();
}

public class ValueChange
{
    public ValueChange(string id, string? value, string? valueMapperId = null)
    {
        Id = id;
        Value = value;
        ValueMapperId = valueMapperId;
    }
    public string Id { get; set; }
    public string? Value { get; set; }
    public string? ValueMapperId { get; set; }
}