using Microsoft.AspNetCore.Mvc;
using Proact.Core;
using Proact.Core.Tag;
using static Proact.Core.Tags;

namespace ProactSandbox.Controllers;

[ApiController]
[Route("[controller]")]
public class HomeController : Controller
{
    [HttpGet]
    public HtmlNode Get()
    {
        var form = DynamicValue.Create<SignUpForm?>("formSubmitted", null);
        var stringValue = DynamicValue.Create("buttonClicked", "");

        var counter = DynamicValue.Create("incrementCounter", 0);

        var inputValue = DynamicValue.Create("onInputChange", "Nothing");
        var list = new List<HtmlNode>()
        {
            p()("First element in a list"),
            p()("Seconds element in a list")
        };

        return html()(
            head(),
            div()(),
            body()().Add(
                div("btn-primary")(
                    p()("Hello World!!"),
                    "Blabla",
                    div(),
                    button(onclick: stringValue.Run())("RefreshTime!!!"),
                    stringValue.Map(() => p()(DateTimeOffset.Now.ToString("O"))),
                    stringValue.Map(() => p()(DateTimeOffset.Now.ToString("T"))),
                    input(name: "firstName", oninput: inputValue.SetFromThisValue()),
                    inputValue.Map(v => p()(v.ToString())),
                    inputValue,
                    list.Select(s => s).ToList(),
                    button(onclick: counter.Set((v, sp) => v + 1))("Increment counter"),
                    counter.Map((v, sp) => span()(v.ToString())),
                    counter,
                    new Form(onsubmit: form.SetOnSubmit())
                    {
                        new Input(type:"text", name:nameof(SignUpForm.FirstName)),
                        new Input(type:"text", name:nameof(SignUpForm.SecondName)),
                        new Input(type:"submit", value: "Submit")
                    },
                    form.Map(v => v == null ? div()("Empty") : div()(v.FirstName + " " + v.SecondName))
                )
            )
        );
    }
}