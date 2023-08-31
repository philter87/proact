using System.Text;

namespace Proact.Core.Tag;

public class RenderState
{
    public IServiceProvider ServiceProvider { get; }
    public List<DynamicValueObject> DynamicValues { get; }
    private readonly StringBuilder _builder = new(128);
    private string _indentation = "";

    public RenderState(IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
        DynamicValues = new List<DynamicValueObject>();
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

    public void AddDynamicHtmlTags(DynamicHtml dynamicHtml)
    {

        DynamicValues.Add(dynamicHtml.GetValue());
    }

    public string GetHtml()
    {
        return _builder.ToString();
    }
}