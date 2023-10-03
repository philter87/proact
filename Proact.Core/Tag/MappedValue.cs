using Proact.Core.Tag.Context;

namespace Proact.Core.Tag;

public class MappedValue<TInput, TReturn> : HtmlNode, IMappedValue
{
    public Func<TInput, IRenderContext, TReturn> ValueMapper { get; set; }
    public string Id { get; set; }
    public IMappedValue? Parent { get; set; }
    public List<IMappedValue> Children { get; set; } = new();

    public MappedValue(Func<TInput, IRenderContext, TReturn> valueMapper, IMappedValue parent) : this(valueMapper, parent, IdUtils.CreateId(valueMapper.Method))
    {
    }
    
    public MappedValue(Func<TInput, IRenderContext, TReturn> valueMapper, IMappedValue parent, string id)
    {
        ValueMapper = valueMapper;
        Parent = parent;
        Id = id;
    }

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
        renderState.AddDynamicHtmlTags(this);
        return renderState;
    }

    public object GetValue(RenderContext renderContext)
    {
        var dependencies = FindDependenciesRecursively(this, new List<IMappedValue>());
        dependencies.Reverse();
        var value = dependencies[0].GetValue(renderContext);
        for (var index = 1; index < dependencies.Count; index++)
        {
            var dependency = dependencies[index];
            value = dependency.MapValue(renderContext, value);
        }

        return value;
    }

    public object? MapValue(RenderContext renderContext, object parentValue)
    {
        return ValueMapper((TInput) parentValue, renderContext);
    }
    
    public MappedValue<TReturn, TMappedValue> Map<TMappedValue>(Func<TReturn, IRenderContext, TMappedValue> valueMapper)
    {
        var value = new MappedValue<TReturn, TMappedValue>(valueMapper, this);
        Children.Add(value);
        return value;
    }

    private List<IMappedValue> FindDependenciesRecursively(IMappedValue currentValue, List<IMappedValue> valuePath)
    {
        valuePath.Add(currentValue);
        var parent = currentValue.Parent; 
        if (parent == null)
        {
            return valuePath;
        }
        return FindDependenciesRecursively(parent, valuePath);
    }
}

public interface IMappedValue
{
    public IMappedValue? Parent { get; set; }
    public string Id { get; set; }
    internal object GetValue(RenderContext renderContext);
    internal object? MapValue(RenderContext renderContext, object parentValue);
    public RenderState Render(RenderState renderState);
    public List<IMappedValue> Children { get; set; }
}