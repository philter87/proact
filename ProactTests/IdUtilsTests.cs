using Proact.Core;
using Proact.Core.Tag;

namespace ProactTests;

public class IdUtilsTests
{
    [Fact]
    public void CreateId()
    {
        ValueSetter<int> valueSetter = (v, sp) => v + 1;
        ValueSetter<int> valueSetter1 = (v, sp) => v+1;
        ValueSetter<int> valueSetter2 = Create();
        ValueSetter<int> valueSetter3 = (v, sp) => v + 2;


        var hash = IdUtils.CreateId(valueSetter.Method);
        var hash1 = IdUtils.CreateId(valueSetter1.Method);
        var hash2 = IdUtils.CreateId(valueSetter2.Method);
        var hash3 = IdUtils.CreateId(valueSetter3.Method);
        
        Assert.Equal(hash, hash1);
        Assert.Equal(hash, hash2);
        Assert.NotEqual(hash, hash3);
    }

    [Fact]
    public void CreateId1()
    {
        Func<HtmlTag> v1 = () => Tags.p().With(DateTimeOffset.Now.ToString("O"));
        Func<HtmlTag> v2 = () => Tags.p().With(DateTimeOffset.Now.ToString("T"));

        var hash = IdUtils.CreateId(v1.Method);
        var hash1 = IdUtils.CreateId(v2.Method);
        
        Assert.NotEqual(hash, hash1);
    }
    
    private ValueSetter<int> Create()
    {
        return (value, _) => value + 1;
    }
}