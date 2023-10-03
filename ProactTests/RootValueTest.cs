using Proact.Core;
using Proact.Core.Tag;

namespace ProactTests;

public class RootValueTest
{
    [Fact]
    public void Set_method()
    {
        var val = DynamicValue.Create("id", "Hello");
        Func<string, string> valueSetter = s => s.ToUpper(); 

        val.Set(valueSetter);

        var renderContext = Any.RenderStateWithValue("id", "Hello", IdUtils.CreateId(valueSetter.Method));
        Assert.Equal("HELLO", val.GetValue(renderContext.RenderContext));
    }
}