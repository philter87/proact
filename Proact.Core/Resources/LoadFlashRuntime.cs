namespace Proact;

public static class LoadFlashRuntime
{
    private const string NamespaceDirectory = "Proact.Core.Resources.";
    public static readonly string FlashJavascriptRuntime = ReadFlashJavascriptRuntime();

    public static string ReadFlashJavascriptRuntime()
    {
        return ReadResource("FlashRuntime.js");
    }
    
    private static string ReadResource(string fileName)
    {
        var namespacePath = NamespaceDirectory + fileName;
        var stream = typeof(LoadFlashRuntime).Assembly.GetManifestResourceStream(namespacePath);
        return new StreamReader(stream).ReadToEnd();
    }
}