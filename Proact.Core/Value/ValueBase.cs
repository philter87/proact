using Proact.Core.Tag;
using Proact.Core.Tag.Context;

namespace Proact.Core;

public abstract class ValueBase<T> : HtmlNode, IMappedValue
{
    public IMappedValue? Parent { get; set; }
    public string Id { get; set; }
    public string RootId { get; set; }
    public List<IMappedValue> Children { get; set; } = new();
    public List<SideEffect> SideEffects { get; set; } = new();
    public abstract object GetValue(RenderContext renderContext);

    public abstract object? MapValue(RenderContext renderContext, object parentValue);
    public override RenderState Render(RenderState renderState)
    {
        var value = GetValue(renderState.RenderContext);
        var htmlNode = Json.MapToTag(value);
        htmlNode.Put(Constants.AttributeDynamicValueId, Id);
        htmlNode.Render(renderState);
        renderState.AddDynamicHtmlTags(this);
        return renderState;
    }

    public void ExecuteSideEffects(RenderContext renderContext)
    {
        if (!renderContext.ValueChanges.ContainsKey(RootId))
        {
            // We skip executing a side effect if the root value has not changed
            return;
        }
        var value = GetValue(renderContext);
        SideEffects.ForEach(s => ExecuteSideEffectIfMissing(renderContext, s, value));

        Parent?.ExecuteSideEffects(renderContext);
    }

    private void ExecuteSideEffectIfMissing(RenderContext renderContext, SideEffect sideEffect, object value)
    {
        if (renderContext.ExecutedSideEffects.Contains(sideEffect.Id))
        {
            return;
        }

        sideEffect.Action(value, renderContext);
        renderContext.ExecutedSideEffects.Add(sideEffect.Id);
    }

    public MappedValue<T, TMappedValue> Map<TMappedValue>(Func<T, IRenderContext, TMappedValue> valueMapper)
    {
        return MapWithId(valueMapper, IdUtils.CreateId(valueMapper.Method));
    }
    
    public MappedValue<T, TMappedValue> Map<TMappedValue>(Func<T, TMappedValue> valueMapper)
    {
        return MapWithId((t, _) => valueMapper(t), IdUtils.CreateId(valueMapper.Method));
    }
    
    public MappedValue<T, TMappedValue> Map<TMappedValue>(Func<TMappedValue> valueMapper)
    {
        return MapWithId((_, _) => valueMapper(), IdUtils.CreateId(valueMapper.Method));
    }

    private MappedValue<T, TMappedValue> MapWithId<TMappedValue>(Func<T, IRenderContext, TMappedValue> valueMapper, string id)
    {
        var mappedValue = new MappedValue<T, TMappedValue>(valueMapper, this, id);
        Children.Add(mappedValue);
        return mappedValue;
    }
}