using Proact.Core;
using Proact.Core.Tag;

namespace ProactTests;

public class IdUtilsTests
{
    [Fact]
    public void Method()
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
    
    private ValueMapper<int> Create()
    {
        return (value, provider) => value + 1;
    }
}