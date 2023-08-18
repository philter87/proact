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
    public void HandleFullRender_simple_html_should_be_rendered_correctly()
    {
        var sut = CreateProactService();
        var tag = div()(
            "String within node",
            p("my-class", style: "Style")("Hello World")
            );

        var html = sut.HandleFullRender(tag);
        
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
    public void HandleFullRender_with_trigger()
    {
        var sut = CreateProactService();
        var trigger = new DynamicValue(TriggerId, DefaultValue);
        var tag = div()(
            trigger.On((v, s) => p()(v?.ToString()))
        );

        var html = sut.HandleFullRender(tag);
        
        AssertEqual($"<div><p data-trigger-id=\"{TriggerId}\">{DefaultValue}</p></div>", html);
    }
    
    [Fact]
    public void HandleFullRender_with_trigger_partial_render()
    {
        var sut = CreateProactService();
        var trigger = new DynamicValue(TriggerId, DefaultValue);
        var tag = div()(
            trigger.On((v, s) => p()(v?.ToString()))
        );
        var newValue = "NewValue";

        sut.HandleFullRender(tag);
        var html = sut.HandlePartialRender(CreateBody(newValue));
        
        AssertEqual($"<p data-trigger-id=\"{TriggerId}\">{newValue}</p>", html?.Html);
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

        var actual = sut.HandleFullRender(tags);
        var expected = sut.HandleFullRender(div("btn-primary")(p()("Hello World!")));
        Assert.Equal(expected, actual);
    }

    private static ValueChangeBody CreateBody(string newValue)
    {
        return new ValueChangeBody()
        {
            TriggerId = TriggerId,
            TriggerOptions = new TriggerOptions()
            {
                Value = newValue,
                IsValueEmpty = false,
            }
        };
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