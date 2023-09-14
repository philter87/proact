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
        var formSubmit = DynamicValue.Create<SignUpForm?>("formSubmitted", null);
        var buttonClick = DynamicValue.Create("buttonClicked", "");

        var counter = DynamicValue.Create("incrementCounter", 0);

        var onInputChange = DynamicValue.Create("onInputChange", "Nothing");
        var list = new List<HtmlNode>()
        {
            p()("First element in a list"),
            p()("Seconds element in a list")
        };

        return html()(
            head(),
            body()(
                div("btn-primary")(
                    p()("Hello World!!"),
                    "Blabla",
                    div(),
                    button(onclick: buttonClick.Run())("RefreshTime!!!"),
                    buttonClick.On(() => p()(DateTimeOffset.Now.ToString("O"))),
                    buttonClick.On(() => p()(DateTimeOffset.Now.ToString("T"))),
                    input(name: "firstName", oninput: onInputChange.SetFromThisValue()),
                    onInputChange.On((v) => p()(v.ToString())),
                    onInputChange,
                    list.Select(s => s).ToList(),
                    button(onclick: counter.Set((v, sp) => v + 1))("Increment counter"),
                    counter.On((v, sp) => span()(v.ToString())),
                    counter,
                    new Form(onsubmit: formSubmit.SetOnSubmit())
                    {
                        new Input(type:"text", name:nameof(SignUpForm.FirstName)),
                        new Input(type:"text", name:nameof(SignUpForm.SecondName)),
                        new Input(type:"submit", value: "Submit")
                    },
                    formSubmit.On(v => v == null ? div()("Empty") : div()(v.FirstName + " " + v.SecondName))
                )
            )
        );
    }
}