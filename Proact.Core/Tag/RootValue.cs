using Proact.Core.Tag.Context;

namespace Proact.Core.Tag;

public class RootValue<T> : HtmlNode, IMappedValue
{
    public RootValue(string id, T initialValue)
    {
        Id = id;
        InitialValue = initialValue;
    }
    
    public RootValue(string id, Func<IRenderContext, T> initialValueCreator)
    {
        Id = id;
        InitialValueCreator = initialValueCreator;
    }

    public string Id { get; set; }
    public T InitialValue { get; set; }
    public Func<IRenderContext, T>? InitialValueCreator { get; set; }
    public IMappedValue? Parent { get; set; } = null;
    
    public override RenderState Render(RenderState renderState)
    {
        var value = GetValue(renderState.RenderContext);
        return RenderValue(renderState, value);
    }
    
    private RenderState RenderValue(RenderState renderState, object renderValue)
    {
        var htmlNode = Json.MapToTag(renderValue);
        htmlNode.Put(Constants.AttributeDynamicValueId, Id);
        htmlNode.Render(renderState);
        return renderState;
    }

    public object? MapValue(RenderContext renderContext, object parentValue)
    {
        // The RootValue has no mapping method
        return parentValue;
    }

    public object GetValue(RenderContext renderContext)
    {
        var triggerOptions = renderContext.GetValueChange(Id);
        if (triggerOptions != null)
        {
            return Json.Parse<T>(triggerOptions.Value);            
        }

        if (InitialValueCreator != null)
        {
            return InitialValueCreator(renderContext);
        }
        return InitialValue;
    }

    public MappedValue<T, TMappedValue> Map<TMappedValue>(Func<T, IRenderContext, TMappedValue> valueMapper)
    {
        var mapped = new MappedValue<T, TMappedValue>(valueMapper);
        mapped.Parent = this;
        return mapped;
    }
}