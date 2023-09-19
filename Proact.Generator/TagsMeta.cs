using System.Collections.Immutable;

namespace Proact.Generator;

public static class TagsMeta
{
    public static readonly IDictionary<string, List<string>> Tags = new Dictionary<string, List<string>>()
    {
        {"html", CreateAttributes("xmlns")},
        {"head", CreateAttributes()},
        {"body", CreateAttributes()},
        {"p", CreateAttributes()},
        {"a", CreateAttributes("href")},
        {"span", CreateAttributes()},
        {"div", CreateAttributes()},
        {"h1", CreateAttributes()},
        {"h2", CreateAttributes()},
        {"h3", CreateAttributes()},
        {"h4", CreateAttributes()},
        {"h5", CreateAttributes()},
        {"h6", CreateAttributes()},
        {"form", CreateAttributes("onsubmit","name", "action", "method", "enctype", "target")},
        {"input", CreateAttributes("type", "name", "oninput", "value")},
        {"button", CreateAttributes("name", "type", "value")},
    }.ToImmutableDictionary();

    private static List<string> CreateAttributes(params string[] additionalAttributes)
    {
        var attributes = new List<string>() { "class" };
        attributes.AddRange(additionalAttributes);
        attributes.AddRange(new []{"style", "id", "hidden=bool", "onclick"});
        return attributes;
    }
}