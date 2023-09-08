using Proact.Core;
using Proact.Core.Tag;

namespace ProactTests;

public class IdUtilsTests
{
    [Fact]
    public void CreateId()
    {
        ValueMapper<int> valueMapper = (v, sp) => v + 1;
        ValueMapper<int> valueMapper1 = (v, sp) => v+1;
        ValueMapper<int> valueMapper2 = Create();
        ValueMapper<int> valueMapper3 = (v, sp) => v + 2;


        var hash = IdUtils.CreateId(valueMapper.Method);
        var hash1 = IdUtils.CreateId(valueMapper1.Method);
        var hash2 = IdUtils.CreateId(valueMapper2.Method);
        var hash3 = IdUtils.CreateId(valueMapper3.Method);
        
        Assert.Equal(hash, hash1);
        Assert.Equal(hash, hash2);
        Assert.NotEqual(hash, hash3);
    }

    [Fact]
    public void CreateId1()
    {
        Func<HtmlTag> v1 = () => Tags.p()(DateTimeOffset.Now.ToString("O"));
        Func<HtmlTag> v2 = () => Tags.p()(DateTimeOffset.Now.ToString("T"));

        var hash = IdUtils.CreateId(v1.Method);
        var hash1 = IdUtils.CreateId(v2.Method);
        
        Assert.NotEqual(hash, hash1);
    }
    
    private ValueMapper<int> Create()
    {
        return (value, provider) => value + 1;
    }
}