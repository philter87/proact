using Proact.Core.Tag;
using Proact.Generator;
using static Proact.Generator.CodeGenerator;

var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Tags.cs");
File.WriteAllText(path, $@"
using {typeof(HtmlNode).Namespace};

namespace {typeof(Proact.Core.Tags).Namespace};

{AddTagsClassWithStaticMethods(TagsMeta.Tags)}
{AddTagClasses(TagsMeta.Tags)}
");