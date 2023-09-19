using System.Text;

namespace Proact.Core.Tag;

public class RenderState
{
    
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
}