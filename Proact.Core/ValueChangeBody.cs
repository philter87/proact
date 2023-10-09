namespace Proact.Core;

public class ValueChangeRender
{
    public string RootId { get; set; }
    public object? Value { get; set; }
    public List<HtmlChange> Changes { get; set; } = new();
    public string? NextUrl { get; set; }
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

public class ValueChangeCommand
{
    public ValueChangeCommand(string id, string? value, string? valueMapperId = null)
    {
        Id = id;
        Value = value;
        ValueMapperId = valueMapperId;
    }
    public string Id { get; set; }
    public string? Value { get; set; }
    public string? ValueMapperId { get; set; }
}