using Proact.Core.Tag;
using Proact.Core.Tag.Context;

namespace ProactTests;

public class MappedValueTest
{
    [Fact]
    public void MapTwice()
    {
        var val = DynamicValue.Create("first", "Philip");

        var mappedValue = val.MapValue((firstName, c) => firstName + " SecondName");

        var renderState = mappedValue.RenderValue(Any.RenderState, "Hans");
        
        Assert.Equal("<span data-dynamic-value-id=\"MK6+ciFz\">Hans SecondName</span>", renderState.GetHtml());
    }
    
    [Fact]
    public void MapSeveral()
    {
        TestMapper("Philip", (v, _) => v + " Hjorth", "Philip Hjorth");
        TestMapper(12, (v, _) => v + 100, "112");
        TestMapper(true, (v, _) => !v, "False");
    }

    public void TestMapper<T, TReturn>(T initialValue, Func<T, IRenderContext, TReturn> Mapper, string expected)
    {
        var val = DynamicValue.Create<T>("id", initialValue);

        var mapped = val.MapValue(Mapper);
        
        var renderState = mapped.RenderValue(Any.RenderState, initialValue);

        Assert.Contains(expected, renderState.GetHtml());
    }
}