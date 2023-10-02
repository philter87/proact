using System.Text;
using Proact.Core.Tag.Context;

namespace Proact.Core.Tag;

public class RenderState
{
    private readonly List<DynamicValueBase> _dynamicValues = new();
    public List<HtmlChange> HtmlChanges { get; } = new();
    public RenderContext RenderContext { get; }

    public RenderState(RenderContext renderContext)
    {
        RenderContext = renderContext;
    }

    private readonly StringBuilder _builder = new(128);

    public RenderState AddLine(string line)
    {
        _builder.Append(line);
        return this;
    }

    public string GetHtml()
    {
        return _builder.ToString();
    }
    
    internal void AddDynamicHtmlTags(DynamicValueBase valueBase)
    {
        _dynamicValues.Add(valueBase);
    }
    
    public List<DynamicValueBase> GetValues()
    {
        return _dynamicValues;
    }

    public void AddHtmlChanges(HtmlChange htmlChange)
    {
        HtmlChanges.Add(htmlChange);
    }

    public void ClearHtml()
    {
        _builder.Clear();
    }
}