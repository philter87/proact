using Microsoft.AspNetCore.Mvc;
using Proact.Html;
using Proact.Tag;
using static Proact.Html.Html;

namespace ProactSandbox.Controllers;

[ApiController]
[Route("[controller]")]
public class HomeController : Controller
{
    [HttpGet]
    public HtmlNode Get()
    {
        var buttonClick = new Trigger<string>("buttonClicked");
        var list = new List<HtmlNode>()
        {
            p()("First element in a list"),
            p()("Seconds element in a list")
        };

        return html()(
            head(),
            body()(
                div("btn-primary")(
                    p()("Hello World!"),
                    "Blabla",
                    div(),
                    button(onclick: buttonClick.Run())("RefreshTime!!!"),
                    buttonClick.On((o,b) => p()(DateTimeOffset.Now.ToString("O"))),
                    list.Select(s => s).ToList()
                )
            )
        );
    }
}