# Proact.Web

### Getting started

Nuget package [Proact.Web](https://www.nuget.org/packages/Proact.Web/)

In the **Program.cs** file add two lines: 

```csharp
...
builder.Services.AddProact();

var app = builder.Build();

app.UseProact();
...
```

Then add a controller
```csharp
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
```