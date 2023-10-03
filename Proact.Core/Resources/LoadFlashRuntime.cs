namespace Proact;

public static class LoadFlashRuntime
{
    private const string NamespaceDirectory = "Proact.Core.Resources.";
    public static readonly string FlashJavascriptRuntime = ReadResource("FlashRuntime.js");
    public static readonly string DevWebSocketConnection = ReadResource("WebSocket.js");
    
    private static string ReadResource(string fileName)
    {
        var namespacePath = NamespaceDirectory + fileName;
        var stream = typeof(LoadFlashRuntime).Assembly.GetManifestResourceStream(namespacePath);
        return new StreamReader(stream).ReadToEnd();
    }
}