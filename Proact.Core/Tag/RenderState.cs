using System.Text;
using Proact.Core.Tag.Context;

namespace Proact.Core.Tag;

public class RenderState
{
    private readonly bool _isPretty;
    public IRenderContext RenderContext { get; }
    public List<DynamicValueObject> DynamicValues { get; }
    private readonly StringBuilder _builder = new(128);
    private string _indentation = "";

    public RenderState(IRenderContext renderContext, bool isPretty = false)
    {
        _isPretty = isPretty;
        RenderContext = renderContext;
        DynamicValues = new List<DynamicValueObject>();
    }
    
    public RenderState AddLine(string line)
    {
        if (_isPretty)
        {
            _builder.AppendLine(_indentation + line);
        }
        else
        {
            _builder.Append(line);
        }
        return this;
    }

    public void LevelIncrement()
    {
        if (!_isPretty)
        {
            return;
        }
        _indentation += "  ";
    }

    public void LevelDecrement()
    {
        if (!_isPretty)
        {
            return;
        }
        _indentation = _indentation[..^2];
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