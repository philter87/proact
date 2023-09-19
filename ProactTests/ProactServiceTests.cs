using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using Proact.Core;
using Proact.Core.Tag;
using static Proact.Core.Tags;

namespace ProactTests;

public class ProactServiceTests
{
    private const string TriggerId = "TriggerId";
    private const string DefaultValue = "DefaultValue";

    [Fact]
    public void Render_simple_html_should_be_rendered_correctly()
    {
        var sut = CreateProactService();

        var tag = div().With(
            "String within node",
            p("my-class", style: "Style").With("Hello World")
        );

        var html = sut.Render(tag);
        
        Assert.Equal(@"<div>String within node<p class=""my-class"" style=""Style"">Hello World</p></div>", html);
    }

    [Fact]
    public void Render_with_dynamic_value()
    {
        var sut = CreateProactService();
        var dynamicValue = DynamicValue.Create(TriggerId, DefaultValue);
        ValueRender<string> valueRender = (v, _) => p().With(v);
        var tag = div().With(
            dynamicValue.Map(valueRender)
        );

        var html = sut.Render(tag);

        var htmlId = IdUtils.CreateId(valueRender.Method);
        Assert.Equal(@$"<div><p data-dynamic-html-id=""{htmlId}"">{DefaultValue}</p></div>", html);
    }
    
    [Fact]
    public void Render_with_different_methods_should_give_the_same_result_accept_for_html_id()
    {
        var val = DynamicValue.Create(TriggerId, DefaultValue);
        
        var html = Render(div().With(val.Map((v, _) => p().With(v))));
        var html1 = Render(div().With(val.Map((v) => p().With(v))));
        var html2 = Render(div().With(val.Map(() => p().With(DefaultValue))));

        Assert.Contains(DefaultValue, html);
        Assert.Contains(DefaultValue, html1);
        Assert.Contains(DefaultValue, html2);
    }

    [Fact]
    public void Render_with_value_mapper()
    {
        var sut = CreateProactService();
        var dynamicValue = DynamicValue.Create(TriggerId, 123);
        ValueMapper<int> valueMapper = (v, _) => v + 1;

        var tag = div().With(
            button(onclick: dynamicValue.Set(valueMapper)),
            dynamicValue.Map((v, _) => p().With(v + ""))
        );

        sut.Render(tag);
        var partialRender =
            sut.RenderPartial(new DynamicValueTriggerOptions(TriggerId, "123", IdUtils.CreateId(valueMapper.Method)));

        Assert.Single(partialRender.IdToHtml);
        foreach (var kv in partialRender.IdToHtml)
        {
            Assert.Contains("124", kv.Value);
        }
    }

    [Fact]
    public void Render_with_trigger_partial_render()
    {
        var sut = CreateProactService();
        var trigger = DynamicValue.Create(TriggerId, DefaultValue);
        ValueRender<string> valueRender = (v, _) => p().With(v);
        var tag = div().With(
            trigger.Map(valueRender)
        );
        
        var newValue = "NewValue";

        sut.Render(tag);
        var html = sut.RenderPartial(CreateBody(newValue));

        var htmlId = IdUtils.CreateId(valueRender.Method);
        Assert.Equal($"<p data-dynamic-html-id=\"{htmlId}\">{newValue}</p>", html?.IdToHtml[htmlId]);
    }

    [Fact]
    public void Render_multiple_dynamic_html_with_same_trigger()
    {
        var sut = CreateProactService();
        var trigger = DynamicValue.Create(TriggerId, DefaultValue);
        var tag = div().With(
            trigger.Map((v, _) => p().With("First " + v)),
            trigger.Map((v, _) => p().With("Second " + v))
        );

        var html = sut.Render(tag);

        Assert.Contains("Second " + DefaultValue, html);
        Assert.Contains("First " + DefaultValue, html);
    }
    
    [Fact]
    public void Render_multiple_dynamic_html_with_different_on_overloads()
    {
        var sut = CreateProactService();
        var trigger = DynamicValue.Create(TriggerId, DefaultValue);
        var tag = div().With(
            trigger.Map((v) => p().With("First " + v)),
            trigger.Map((v) => p().With("Second " + v))
        );

        var html = sut.Render(tag);

        Assert.Contains("Second " + DefaultValue, html);
        Assert.Contains("First " + DefaultValue, html);
    }

    [Fact]
    public void PartialRender_form_submit()
    {
        var signUpForm = DynamicValue.Create<NameForm>(TriggerId, null);
        var nameForm = new NameForm() { FirstName = "Philip", SecondName = "Christiansen" };
        var tag = div().With(
            signUpForm.Map((v, _) => div().With(v == null ? "Nothing" : v.FirstName + " " + v.SecondName))
        );

        var partialRenderedHtml = PartialRenderWithValue(tag, nameForm);
        
        Assert.Contains("Philip Christiansen", partialRenderedHtml);
    }
    
    [Fact]
    public void Render_with_nested_dynamic_values()
    {
        var condition = DynamicValue.Create("condition", true);
        var nestedValue = DynamicValue.Create("nestedValue", 23423);
        var tag = div().With(condition.Map(v => v ? span().With("This is rendered first") : span().With(nestedValue)));
        var sut = CreateProactService();

        sut.Render(tag);
        var result = sut.RenderPartial(new DynamicValueTriggerOptions("condition", "false"));

        var newHtml = result.IdToHtml.First().Value;
        Assert.Contains("23423", newHtml);
    }
    
    [Fact]
    public void Render_with_nested_dynamic_value_which_that_change()
    {
        var condition = DynamicValue.Create("condition", true);
        var nestedValue = DynamicValue.Create("nestedValue", 23423);
        var tag = div().With(condition.Map(v => v ? span().With("This is rendered first") : span().With(nestedValue)));
        var sut = CreateProactService();

        sut.Render(tag);
        sut.RenderPartial(new DynamicValueTriggerOptions("condition", "false"));
        var result = sut.RenderPartial(new DynamicValueTriggerOptions("nestedValue", "5646987"));

        var newHtml = result.IdToHtml.First().Value;
        Assert.Contains("5646987", newHtml);
    }

    private string PartialRenderWithValue<T>(HtmlTag tag, T value)
    {
        var sut = CreateProactService();
        var valueAsString = JsonSerializer.Serialize(value);
        
        sut.Render(tag);
        var partialRender = sut.RenderPartial(CreateBody(valueAsString));

        return partialRender.IdToHtml.Values.ToList()[0];
    }

    private string Render(HtmlTag tag)
    {
        var sut = CreateProactService();
        return sut.Render(tag);
    }

    private static DynamicValueTriggerOptions CreateBody(string newValue)
    {
        return new DynamicValueTriggerOptions(TriggerId, newValue);
    }

    private static ProactService CreateProactService()
    {
        var serviceProvider = new ServiceCollection().BuildServiceProvider();
        ProactService sut = new ProactService(serviceProvider);
        return sut;
    }
}