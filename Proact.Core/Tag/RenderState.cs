using System.Text;

namespace Proact.Core.Tag;

public class RenderState
{
    public IServiceProvider ServiceProvider { get; }
    public List<DynamicHtml> DynamicHtmlTags { get; }
    private readonly StringBuilder _builder = new();
    private string _indentation = "";

    public RenderState(IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
        DynamicHtmlTags = new List<DynamicHtml>();
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

    public void AddTriggeredHtmlTag(DynamicHtml dynamicHtml)
    {
        DynamicHtmlTags.Add(dynamicHtml);
    }

    public string GetHtml()
    {
        return _builder.ToString();
    }
}