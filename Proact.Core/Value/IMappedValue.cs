using Proact.Core.Tag;
using Proact.Core.Tag.Context;

namespace Proact.Core.Value;

public interface IMappedValue
{
    internal IMappedValue? Parent { get; set; }
    internal string Id { get; set; }
    internal string RootId { get; set; }
    internal object GetValue(RenderContext renderContext);
    internal object? MapValue(RenderContext renderContext, object parentValue);
    internal RenderState Render(RenderState renderState);
    internal void ExecuteSideEffects(RenderContext renderContext);
    internal List<IMappedValue> Children { get; set; }
}