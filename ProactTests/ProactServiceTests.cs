using System.Text.Json;
using System.Text.RegularExpressions;
using Microsoft.Extensions.DependencyInjection;
using Proact;
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

        var tag = div()(
            "String within node",
            p("my-class", style: "Style")("Hello World")
        );

        var html = sut.Render(tag);

        const string expectedHtml =
            @"<div>
  String within node
  <p class=""my-class"" style=""Style"">
    Hello World
  </p>
</div>
";
        Assert.Equal(expectedHtml, html);
    }

    [Fact]
    public void Render_with_dynamic_value()
    {
        var sut = CreateProactService();
        var dynamicValue = DynamicValue.Create(TriggerId, DefaultValue);
        ValueRender<string> valueRender = (v, s) => p()(v);
        var tag = div()(
            dynamicValue.Map(valueRender)
        );

        var html = sut.Render(tag);

        var htmlId = IdUtils.CreateId(valueRender.Method);
        AssertEqual($"<div><p data-dynamic-html-id=\"{htmlId}\">{DefaultValue}</p></div>", html);
    }
    
    [Fact]
    public void Render_with_different_methods_should_give_the_same_result_accept_for_html_id()
    {
        var val = DynamicValue.Create(TriggerId, DefaultValue);
        
        var html = Render(div()(val.Map((v, s) => p()(v))));
        var html1 = Render(div()(val.Map((v) => p()(v))));
        var html2 = Render(div()(val.Map(() => p()(DefaultValue))));

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

        var tag = div()(
            button(onclick: dynamicValue.Set(valueMapper)),
            dynamicValue.Map((v, s) => p()(v + ""))
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
        ValueRender<string> valueRender = (v, s) => p()(v);
        var tag = div()(
            trigger.Map(valueRender)
        );
        
        var newValue = "NewValue";

        sut.Render(tag);
        var html = sut.RenderPartial(CreateBody(newValue));

        var htmlId = IdUtils.CreateId(valueRender.Method);
        AssertEqual($"<p data-dynamic-html-id=\"{htmlId}\">{newValue}</p>", html?.IdToHtml[htmlId]);
    }

    [Fact]
    public void Render_multiple_dynamic_html_with_same_trigger()
    {
        var sut = CreateProactService();
        var trigger = DynamicValue.Create(TriggerId, DefaultValue);
        var tag = div()(
            trigger.Map((v, s) => p()("First " + v)),
            trigger.Map((v, s) => p()("Second " + v))
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
        var tag = div()(
            trigger.Map((v) => p()("First " + v)),
            trigger.Map((v) => p()("Second " + v))
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
        var tag = div()(
            signUpForm.Map((v, sp) => div()(v == null ? "Nothing" : v.FirstName + " " + v.SecondName))
        );

        var partialRenderedHtml = PartialRenderWithValue(tag, nameForm);
        
        Assert.Contains("Philip Christiansen", partialRenderedHtml);
    }

    [Fact]
    public void Static_method_calls_and_new_class_is_the_same()
    {
        var sut = CreateProactService();
        var tags = new Div("btn-primary")
        {
            new P()
            {
                "Hello World!"
            },
        };

        var actual = sut.Render(tags);
        var expected = sut.Render(div("btn-primary")(p()("Hello World!")));
        Assert.Equal(expected, actual);
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

    private void AssertEqual(string expectedString, string? html)
    {
        html = RemoveDoubleSpace(RemoveLineBreaks(html));
        Assert.Equal(expectedString, html);
    }

    private static string RemoveDoubleSpace(string html)
    {
        const string doubleSpace = "  ";
        return Regex.Replace(html, @doubleSpace, "");
    }

    private static string RemoveLineBreaks(string html)
    {
        return Regex.Replace(html, @"\r\n", "");
    }
}