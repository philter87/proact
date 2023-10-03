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

    [Fact]
    public void TriggerValueChange()
    {
        var val = DynamicValue.Create("id", false);
        var anotherValue = DynamicValue.Create("name", "Empty");
        var tag = Tags.div().With(
            anotherValue,
            val.Map((b, c) =>
            {
                if (b)
                {
                    c.TriggerValueChange(anotherValue, "Philip");
                }

                return "";
            }));
        
        
        var renderState = tag.Render(Any.RenderStateWithValue("id", "True"));
        Assert.NotEmpty(renderState.RenderContext.ServerValueChanges);
    }
    
    
    [Fact]
    public void SideEffects_should_not_be_executed_on_initial_render()
    {
        var val = DynamicValue.Create("id", false);
        var callCount = 0;

        val.OnChange((v, c) => callCount++);
        val.Render(Any.RenderState);
        
        Assert.Equal(0, callCount);
    }
    
    [Fact]
    public void SideEffects_should_execute_when_value_is_changed_after_initial_render()
    {
        var val = DynamicValue.Create("id", false);
        var callCount = 0;

        val.OnChange((v, c) => callCount++);
        val.Render(Any.RenderStateWithValue("id", "False"));
        
        Assert.Equal(1, callCount);
    }
}