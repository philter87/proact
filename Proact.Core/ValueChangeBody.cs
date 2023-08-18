namespace Proact;

public class ValueChangeBody
{
    public TriggerOptions? TriggerOptions { get; set; }
    public string TriggerId { get; set; }
}

public class DynamicHtmlResult
{
    public object? InitialValue { get; set; }
    public object? Value { get; set; }
    public string? Html { get; set; }
}

public class TriggerOptions
{
    public string Value { get; set; }
    public bool IsValueEmpty { get; set; }
    public bool IsValueMapper { get; set; }
}