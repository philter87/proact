using System.Text;
using Proact.Core.Tag.Context;

namespace Proact.Core.Tag;

public class RenderState
{
    public IRenderContext RenderContext { get; }
    public List<DynamicValueObject> DynamicValues { get; }
    private readonly StringBuilder _builder = new(128);

    public RenderState(IRenderContext renderContext)
    {
        RenderContext = renderContext;
        DynamicValues = new List<DynamicValueObject>();
    }
    
    public RenderState AddLine(string line)
    {
        _builder.Append(line);
        return this;
    }

    public void AddDynamicHtmlTags(DynamicHtml dynamicHtml)
    {
        DynamicValues.Add(dynamicHtml.GetValue());
    }

    public string GetHtml()
    {
        return _builder.ToString();
    }
}