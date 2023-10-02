﻿using Proact.Core.Tag;
using Proact.Core.Tag.Context;

namespace ProactTests;

public class MappedValueTest
{
    [Fact]
    public void GetValue_when_parent_is_Philip_and_mapping_adds_C_we_should_get_PhilipC()
    {
        var val = new RootValue<string>("Name", "Philip");
        var mappedValue = val.Map((firstName, c) => firstName + "C");

        var value = mappedValue.GetValue(Any.RenderContextDefault);

        Assert.Equal("PhilipC", value);
    }
    
    
    [Fact]
    public void GetValue_with_several_mappings()
    {
        var mappedValue = new RootValue<string>("Initials","P").Map((v, c) => v + "H").Map((v, c) => v + "C");

        var value = mappedValue.GetValue(Any.RenderContextDefault);

        Assert.Equal("PHC", value);
    }
    
    [Fact]
    public void GetValue_from_context()
    {
        var val = new RootValue<string>("Name", "IsNotUsedBecauseWeSupplyWithAValueInRenderContext");
        var mappedValue = val.Map((firstName, c) => firstName + "C");
        var renderState = Any.RenderStateWithValue("Name", "Pete");

        var value = mappedValue.GetValue(renderState.RenderContext);

        Assert.Equal("PeteC", value);
    }

    [Fact]
    public void Render_a_mapped_value_of_Phil_and_Christiansen()
    {
        var rootValue = new RootValue<string>("Name", "Phil");
        var mappedValue = rootValue.Map((v, c) => v + " Christiansen");

        var renderState = mappedValue.Render(Any.RenderState);

        HtmlTestUtils.Equal("<span>Phil Christiansen</span>", renderState);
    }
    
    [Fact]
    public void PartialRender_a_mapped_value_of_Pete_and_Christiansen()
    {
        var firstName = new RootValue<string>("Name", "Phil");
        var fullName = firstName.Map((v, c) => v + " Christiansen");

        var renderState = fullName.Render(Any.RenderStateWithValue("Name", "Pete"));

        HtmlTestUtils.Equal("<span>Pete Christiansen</span>", renderState);
    }
    
    [Fact]
    public void Render_after_several_mappings()
    {
        var firstName = new RootValue<string>("Name", "ThisIsOverriden");
        var middleName = firstName.Map((v, c) => v + " Hjorth");
        var fullName = middleName.Map((v, c) => v + " Christiansen");

        var renderState = fullName.Render(Any.RenderStateWithValue("Name", "Philip"));

        HtmlTestUtils.Equal("<span>Philip Hjorth Christiansen</span>", renderState);
    }
    
    [Fact]
    public void RenderSpecificId()
    {
        var name = new RootValue<string>("first", "Philip");
        var fullName = name.Map((firstName, c) => firstName + " C");

        var renderState = fullName.Render(Any.RenderState);
        
        Assert.Equal("<span data-dynamic-value-id=\"NRJcpJy8\">Philip C</span>", renderState.GetHtml());
    }
    
    [Fact]
    public void MapSeveralTypes()
    {
        TestMapper("Philip", (v, _) => v + " Hjorth", "Philip Hjorth");
        TestMapper(12, (v, _) => v + 100, "112");
        TestMapper(true, (v, _) => !v, "False");
    }
    
    [Fact]
    public void Render_input_123_is_converted_to_133_in_child()
    {
        var parent = new RootValue<int>("value", 123);
        var child = parent.Map((i, c) => i + 10);

        HtmlTestUtils.Equal("<span>133</span>", child.Render(Any.RenderState));
    }

    private static void TestMapper<T, TReturn>(T initialValue, Func<T, IRenderContext, TReturn> Mapper, string expected)
    {
        var val = new RootValue<T>("id", initialValue);
        var mapped = val.Map(Mapper);
        
        var renderState = mapped.Render(Any.RenderState);

        Assert.Contains(expected, renderState.GetHtml());
    }
}