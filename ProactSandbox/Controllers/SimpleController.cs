using Microsoft.AspNetCore.Mvc;
using Proact.Core.Tag;
using static Proact.Core.Tags;

namespace ProactSandbox.Controllers;

[ApiController]
public class SimpleController : Controller
{
    [Route("/simple")]
    public HtmlNode Get()
    {
        return div().With("Hello simple!!!");
    }
}