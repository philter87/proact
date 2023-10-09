using Microsoft.AspNetCore.Mvc;
using Proact.Core;
using Proact.Core.Tag;
using Proact.Core.Value;
using static Proact.Core.ProactTags;
using static Proact.Core.Tags;
using Route = Proact.Core.Tag.Route;

namespace ProactSandbox.Controllers;

[ApiController]
public class HomeController : Controller
{
    [Route("/{*AnyPath}")]
    public HtmlNode Get()
    {
        var form = DynamicValue.Create<SignUpForm?>("formSubmitted", null);
        var stringValue = DynamicValue.Create("buttonClicked", "");

        var counter = DynamicValue.Create("incrementCounter", 0);

        var inputValue = DynamicValue.Create("onInputChange", "Nothing");
        var list = new List<HtmlNode>()
        {
            p().With("First element in a list"),
            p().With("Seconds element in a list")
        };

        return html().With(
            head(),
            div().With(),
            body().With(
                div("btn-primary").With(
                    p().With("Hello World"),
                    div(),
                    "Blabla",
                    div(),
                    button(onclick: stringValue.Run()).With("RefreshTime!!!"),
                    stringValue.Map(() => p().With(DateTimeOffset.Now.ToString("O"))),
                    stringValue.Map(() => p().With(DateTimeOffset.Now.ToString("T"))),
                    input(name: "firstName", oninput: inputValue.SetFromThisValue()),
                    inputValue.Map(v => p().With(v.ToString())),
                    inputValue,
                    list.Select(s => s).ToList(),
                    button(onclick: counter.Set(v => v + 1)).With("Increment counter"),
                    counter.Map(v => span().With(v.ToString())),
                    counter,
                    new Form(onsubmit: form.SetOnSubmit())
                    {
                        new Input(type:"text", name:nameof(SignUpForm.FirstName)),
                        new Input(type:"text", name:nameof(SignUpForm.SecondName)),
                        new Input(type:"submit", value: "Submit")
                    },
                    form.Map(v => v == null ? div().With("Empty") : div().With(v.FirstName + " " + v.SecondName)),
                    Routes.Create(
                        new Route("/", div().With("Home")),
                        new Route("/about", div().With("About")),
                        new Route("/contact", div().With("Contact"))
                        ),
                    div().With(
                        h1().With("Links"),
                        Link(href: "/").With("Home"),
                        Link(href: "/about").With("About"),
                        Link(href: "/contact").With("Contact")
                        )
                )
            )
        );
    }
}