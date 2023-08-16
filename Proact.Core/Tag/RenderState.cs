using System.Text;

namespace Proact.Core.Tag;

public class RenderState
{
    public IServiceProvider ServiceProvider { get; }
    public List<HtmlDynamic> TriggeredHtmlTags { get; }
    private readonly StringBuilder _builder = new();
    private string _indentation = "";

    public RenderState(IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
        TriggeredHtmlTags = new List<HtmlDynamic>();
    }

    public RenderState Add(string str)
    {
        _builder.Append(str);
        return this;
    }
    
    public RenderState AddLine(string line)
    {
        _builder.AppendLine(_indentation + line);
        return this;
    }

    public void LevelIncrement()
    {
        _indentation += "  ";
    }

    public void LevelDecrement()
    {
        _indentation = _indentation[..^2];
    }

    public void AddTriggeredHtmlTag(HtmlDynamic htmlDynamic)
    {
        TriggeredHtmlTags.Add(htmlDynamic);
    }

    public string GetHtml()
    {
        return _builder.ToString();
    }
}