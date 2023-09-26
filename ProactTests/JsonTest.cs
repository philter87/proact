using Proact.Core;

namespace ProactTests;

public class JsonTest
{
    [Fact]
    public void Parse()
    {
        Assert.Equal("abc", Json.Parse<string>("abc"));
        Assert.Equal("abc", Json.AsString("abc"));
        Assert.Equal("1", Json.AsString(1));
        Assert.Equal(1, Json.Parse<int>("1"));
        Assert.Equal("False", Json.AsString(false));
        Assert.False(Json.Parse<bool>("False"));
    }
}