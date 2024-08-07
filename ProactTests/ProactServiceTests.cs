using Microsoft.Extensions.DependencyInjection;
using Proact.Core;
using Proact.Core.Tag;
using Proact.Core.Tag.Context;
using Proact.Core.Value;
using static Proact.Core.Tags;

namespace ProactTests;

public class ProactServiceTests
{
    private const string TriggerId = "TriggerId";
    private const string DefaultValue = "DefaultValue";

    [Fact]
    public void Render_with_dynamic_value()
    {
        
        var dynamicValue = DynamicValue.Create(TriggerId, DefaultValue);
        Func<string, IRenderContext, HtmlTag> valueMapper = (v, _) => p().With(v);
        var tag = div().With(
            dynamicValue.Map(valueMapper)
        );

        var html = tag.Render(Any.RenderState);

        var htmlId = IdUtils.CreateId(valueMapper.Method);
        Assert.Equal(@$"<div><p data-dynamic-value-id=""{htmlId}"">{DefaultValue}</p></div>", html.GetHtml());
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
        Func<int, int> valueSetter = v => v + 1;
    
        var tag = div().With(
            button(onclick: dynamicValue.Js.Set(valueSetter)),
            dynamicValue.Map((v, _) => p().With(v + ""))
        );
    
        sut.Render(tag);
        var partialRender = sut.RenderPartial(Any.RenderStateWithValue(TriggerId, "123", IdUtils.CreateId(valueSetter.Method)));

        Assert.Contains("124", partialRender[0].Changes[0].Html);
    }
    

    [Fact]
    public void Render_with_trigger_partial_render()
    {
        var value = DynamicValue.Create(TriggerId, DefaultValue);
        Func<string, IRenderContext, HtmlTag> valueMapper = (v, _) => p().With(v);
        var tag = div().With(
            value.Map(valueMapper)
        );
        
        var newValue = "NewValue";
        var html = PartialRenderWithValue(tag, newValue);

        var htmlId = IdUtils.CreateId(valueMapper.Method);
        Assert.Equal($"<p data-dynamic-value-id=\"{htmlId}\">{newValue}</p>", html);
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
    public void PartialRender_form_submit1()
    {
        var signUpForm = DynamicValue.Create<NameForm>(TriggerId, null);
        var nameForm = new NameForm() { FirstName = "Philip", SecondName = "Christiansen" };
        var tag = div().With(
            signUpForm
        );

        var partialRenderedHtml = PartialRenderWithValue(tag, nameForm);

        var expected =
            "<span data-dynamic-value-id=\"TriggerId\">{\"FirstName\":\"Philip\",\"SecondName\":\"Christiansen\"}</span>";
        Assert.Equal(expected, partialRenderedHtml);
    }
    
    [Fact]
    public void PartialRender_form_submit2()
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
        var result = sut.RenderPartial(Any.RenderStateWithValue("condition", "false"));
        
        Assert.Contains("23423", result[0].Changes[0].Html);
    }
    
    [Fact]
    public void Render_with_nested_dynamic_value_which_that_change()
    {
        var condition = DynamicValue.Create("condition", true);
        var nestedValue = DynamicValue.Create("nestedValue", 23423);
        var tag = div().With(condition.Map(v => v ? span().With("This is rendered first") : span().With(nestedValue)));
        var sut = CreateProactService();
        
        sut.Render(tag);
        sut.RenderPartial(Any.RenderStateWithValue("condition", "false"));
        var result = sut.RenderPartial(Any.RenderStateWithValue("nestedValue", "5646987"));
        
        Assert.Contains("5646987", result[0].Changes[0].Html);
    }
    
    [Fact]
    public void When_firstName_is_changed_another_value_is_changed_with_TriggerValueChange_on_the_server()
    {
        var firstName = DynamicValue.Create("first", "Phil");
        var lastName = DynamicValue.Create("shouldNavigate", "NotRelevant");
        var tag = div().With(
            firstName.Map((v, c) =>
            {
                c.TriggerValueChange(lastName, "Christiansen");
                return "Philip";
            }),
            lastName
        );

        var renders = PartialRenderAll(tag, "first", "Philip");

        Assert.Contains("Christiansen", renders[1].Changes[0].Html);
    }
    
    [Fact]
    public void When_firstName_is_changed_then_secondName_is_changed_with_OnChange()
    {
        var firstName = DynamicValue.Create("first", "Phil");
        var lastname = DynamicValue.Create("secondName", "NotRelevant");
        
        firstName.OnChange((v,c ) => c.TriggerValueChange(lastname,"Christiansen"));
        var tag = div().With(
            firstName,
            lastname
        );
        
        var renders = PartialRenderAll(tag, "first", "Philip");
        Assert.Contains("Christiansen", renders[1].Changes[0].Html);
    }
    
    [Fact]
    public void When_firstName_is_changed_then_change_the_same_value_withOnChange_but_OnChange_should_only_be_called_once()
    {
        var firstName = DynamicValue.Create("first", "");
        var functionCount = 0;
        
        firstName.OnChange((v,c ) =>
        {
            functionCount++;
            c.TriggerValueChange(firstName, "Philip");
        });
        var tag = div().With(
            firstName
        );
        
        var renders = PartialRenderAll(tag, "first", "Phil");
        Assert.Contains("Philip", renders[0].Changes[0].Html);
        Assert.Equal(1, functionCount);
    }

    [Fact]
    public void Navigate_is_called_when_name_is_changed_and_it_causes_the_route_to_change()
    {
        var name = DynamicValue.CreateQueryParameter("name", "Philip");
        var routes = Routes.Create(
            new Route("/hello", div().With("Hello: ", name)),
            new Route("/anotherRoute", div().With("AnotherRoute"))
            );
        
        name.OnChange((v, c) => c.Navigate("/anotherRoute"));
        var result = PartialRenderAll(routes, "name", "PHILIP");
        
        HtmlTestUtils.Equal("<div>AnotherRoute</div>", result[1].Changes[0]);
        // Assert.Equal("/anotherRoute", result[1].NextUrl);
    }

    private string PartialRenderWithValue<T>(HtmlTag tag, T value)
    {
        return PartialRenderWithValue(tag, TriggerId, value);
    }
    private string PartialRenderWithValue<T>(HtmlTag tag, string id, T value)
    {
        var sut = CreateProactService();
        var html = sut.Render(tag);
        Assert.NotNull(html);
        var partialRender = sut.RenderPartial(Any.RenderStateWithValue(id, value));

        return partialRender[0].Changes[0].Html;
    }
    
    private List<ValueChangeRender> PartialRenderAll<T>(HtmlTag tag, string id, T value)
    {
        var sut = CreateProactService();
        var html = sut.Render(tag);
        Assert.NotNull(html);
        return sut.RenderPartial(Any.RenderStateWithValue(id, value));
    }
    
    

    private string Render(HtmlTag tag)
    {
        var sut = CreateProactService();
        return sut.Render(tag);
    }

    private static ProactService CreateProactService()
    {
        var serviceProvider = new ServiceCollection().BuildServiceProvider();
        ProactService sut = new ProactService(serviceProvider);
        return sut;
    }
}