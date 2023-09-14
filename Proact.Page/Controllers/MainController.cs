using Microsoft.AspNetCore.Mvc;
using Proact.Core;
using Proact.Core.Tag;

namespace Proact.Page.Controllers;

[ApiController]

public class MainController : ControllerBase
{
    
    [Route("/")]
    public HtmlTag Get()
    {
        return new Html()
        {
            new Head(),
            new Body
            {
                new Div{"Hello World!!!"}
            }
        };
    }
}