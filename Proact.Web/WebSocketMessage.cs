namespace ProactSandbox.Controllers;

public class WebSocketMessage
{
    public WebSocketMessage(string type)
    {
        Type = type;
    }

    public static WebSocketMessage CreateHotReload()
    {
        return new WebSocketMessage("HotReload");
    }

    public string Type { get; set; }
}