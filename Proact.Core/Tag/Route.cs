namespace Proact.Core.Tag;

public class Route
{
    public string Path { get; set; }
    public HtmlTag Tag { get; set; }

    public Route(string path, HtmlTag tag)
    {
        Path = path;
        Tag = tag;
    }
}