using Proact.Core.Tag;

namespace Proact.Generator;

public static class CodeGenerator
{
    public static string AddTagClasses(IDictionary<string, List<string>> tags)
    {
        return string.Join("",tags.Select(kv => AddTagClass(kv.Key, kv.Value)));
    }

    private static string AddTagClass(string tag, List<string> attributesShorthand)
    {
        var tagClassName = tag.Substring(0, 1).ToUpper() + tag.Substring(1);
        var attributes = Convert(attributesShorthand);
        return $@"
public class {tagClassName} : HtmlTag
{{
    public {tagClassName}({CreateParameters(attributes)}) : base(""{tag}"")
    {{
        this{AddAttributes(attributes)};
    }}
}}
";
    }
    public static string AddTagsClassWithStaticMethods(IDictionary<string, List<string>> tags)
    {
        return $@"
public static class Tags
{{
{AddStaticMethods(tags)}
}}
";
    }

    private static string AddStaticMethods(IDictionary<string, List<string>> tags)
    {
        return string.Join("", tags.Select(kv => AddStaticMethod(kv.Key, kv.Value)));
    }
    private static string AddStaticMethod(string tag, List<string> attributesShort)
    {
        var attributes = Convert(attributesShort);
        
        return $@"
    public static {nameof(HtmlTagFunc)} {tag}({CreateParameters(attributes)})
    {{
        return children => new {nameof(HtmlTag)}(""{tag}"", children){AddAttributes(attributes)};
    }}
";
    }

    private static string AddAttributes(List<HtmlAttribute> attributes)
    {
        return string.Join("", attributes.Select(attr => attr.AsAddAttribute()));
    }

    private static string CreateParameters(List<HtmlAttribute> attributes)
    {
        return string.Join(", ", attributes.Select(a => a.AsParameter()));
    }

    private static List<HtmlAttribute> Convert(List<string> attributes)
    {
        return attributes
            .Distinct()
            .Select(attr => new HtmlAttribute(attr))
            .ToList();
    }
    
    private sealed class HtmlAttribute
    {
        public HtmlAttribute(string shorthand)
        {
            var words = shorthand.Split("=");
            HtmlName = words[0];
            FieldName = HtmlName == "class" ? "klass" : HtmlName; 
            Type = words.Length == 1 ? "string" : words[1];
        }

        private string FieldName { get; }
        private string HtmlName { get; }
        private string Type { get; }

        public string AsParameter()
        {
            return $"{Type}? {FieldName} = null";
        }

        public string AsAddAttribute()
        {
            return $".{nameof(HtmlTag.Put)}(\"{HtmlName}\",{FieldName})";
        }
    }
}

