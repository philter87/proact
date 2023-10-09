using Proact.Core.Tag.Context;

namespace Proact.Core.Value;

public class RootValueOptions<T> 
{
    public Func<IRenderContext, T>? InitialValueCreator { get; init; }
    public T? InitialValue { get; init; }
    public bool IsQueryParameter { get; set; } = false;
}