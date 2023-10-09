using Proact.Core;
using Proact.Core.Tag;
using Proact.Core.Value;
using static Proact.Core.Tags;

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
        var tag = div().With(
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
        var renderState = Any.RenderStateWithValue("id", "False"); 
        var callCount = 0;

        val.OnChange((v, c) => callCount++);
        val.ExecuteSideEffects(renderState.RenderContext);
        
        Assert.Equal(1, callCount);
    }
    
    [Fact]
    public void SideEffects_should_execute_on_a_mapped_value()
    {
        var val = DynamicValue.Create("id", false);
        var renderState = Any.RenderStateWithValue("id", "True"); 
        var callCount = 0;

        var valStr = val.Map(v => v.ToString());
        valStr.OnChange((v, c) => callCount++);
        valStr.ExecuteSideEffects(renderState.RenderContext);
        
        Assert.Equal(1, callCount);
    }

    [Fact]
    public void Cache_should_be_used_so_mapper_functions_are_not_executed_too_many_times()
    {
        var functionCalls = 0;
        var firstName = DynamicValue.Create("id", "Phil");
        var middleName = firstName.Map(v =>
        {
            functionCalls++;
            return v + " Hjorth";
        });
        var fullName = middleName.Map(v =>
        {
            functionCalls++;
            return v + " Christiansen";
        });

        var renderContext = Any.RenderContextDefault;
        var v = fullName.GetValue(renderContext);
        v = fullName.GetValue(renderContext);
        
        Assert.Equal(2, functionCalls);
        Assert.Equal("Phil Hjorth Christiansen", v);
    }
    
}