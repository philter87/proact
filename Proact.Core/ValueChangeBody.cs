namespace Proact.Core;
public class DynamicHtmlResult
{
    public object? Value { get; set; }
    public List<HtmlChange> HtmlChanges { get; set; } = new();
}

public class HtmlChange
{
    public HtmlChange()
    {
    }

    public HtmlChange(string id, string html)
    {
        Id = id;
        Html = html;
    }

    public string Id { get; set; } = "";
    public string Html { get; set; } = "";
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