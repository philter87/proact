namespace Proact;
public class DynamicHtmlResult
{
    public object? InitialValue { get; set; }
    public object? Value { get; set; }
    public string? Html { get; set; }
}

public class TriggerOptions
{
    public TriggerOptions(string id)
    {
        Id = id;
    }

    public TriggerOptions()
    {
    }

    public string Id { get; set; }
    public string? Value { get; set; }
    
    public bool IsValueMapper { get; set; }
}