using Proact.Core.Tag.Context;

namespace Proact.Core.Tag;

public class SideEffect
{
    public static SideEffect Create<T>(Action<T, IRenderContext> action)
    {
        return new SideEffect()
        {
            Id = IdUtils.CreateId(action.Method),
            Action = (v, c) => action((T)v, c)
        };
    }

    public string Id { get; set; }
    public Action<object, IRenderContext> Action { get; set; }
}