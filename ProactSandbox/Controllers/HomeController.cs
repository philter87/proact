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
        var buttonClick = new DynamicValue("buttonClicked");

        var counter = new DynamicValue("incrementCounter", 0);

        var onInputChange = new DynamicValue("onInputChange", "Nothing");
        var list = new List<HtmlNode>()
        {
            p()("First element in a list"),
            p()("Seconds element in a list")
        };

        return html()(
            head(),
            body()(
                new Div()
                {
                    div()
                },
                div("btn-primary")(
                    p()("Hello World!"),
                    "Blabla",
                    div(),
                    button(onclick: buttonClick.Run())("RefreshTime!!!"),
                    buttonClick.On((o, b) => p()(DateTimeOffset.Now.ToString("O"))),
                    input(name: "firstName", oninput: onInputChange.RunUseThisValue()),
                    onInputChange.On((v, sp) => p()(v.ToString())),
                    list.Select(s => s).ToList(),
                    button(onclick: counter.Run((v, sp) => (int.Parse(v.ToString()) + 1).ToString()))("Increment counter"),
                    counter.On((v, sp) => span()(v.ToString()))
                )
            )
        );
    }
}