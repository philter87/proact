using Proact.Core.Tag;
using Proact.Generator;
using static Proact.Generator.CodeGenerator;

var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Tags.cs");
File.WriteAllText(path, $@"
using {typeof(HtmlNode).Namespace};

namespace {typeof(Proact.Core.Tags).Namespace};

{AddStaticMethodsToTagsClass(TagsMeta.Tags)}

{AddTagClasses(TagsMeta.Tags)}
");


var path2 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "OldTags.cs");
File.WriteAllText(path2, $@"
using {typeof(HtmlNode).Namespace};

namespace {typeof(Proact.Core.Tags).Namespace}.Old;

{AddStaticMethodsToOldTagsClass(TagsMeta.Tags)}

");

