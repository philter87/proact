using Proact.Core.Tag;
using Proact.Core.Tag.Context;

namespace Proact.Core.Value;

public class MappedValue<TInput, TReturn> : ValueBase<TInput>
{
    private Func<TInput, IRenderContext, TReturn> ValueMapper { get; set; }

    public MappedValue(Func<TInput, IRenderContext, TReturn> valueMapper, IMappedValue parent) : this(valueMapper, parent, IdUtils.CreateId(valueMapper.Method))
    {
    }
    
    public MappedValue(Func<TInput, IRenderContext, TReturn> valueMapper, IMappedValue parent, string id)
    {
        ValueMapper = valueMapper;
        Parent = parent;
        RootId = parent.RootId;
        Id = id;
    }

    public override object GetValue(RenderContext renderContext)
    {
        if (renderContext.CalculatedValues.TryGetValue(Id, out var cachedValue))
        {
            return cachedValue;
        }
        var dependencies = FindDependenciesRecursively(this, new List<IMappedValue>());
        var value = dependencies[0].GetValue(renderContext);
        for (var index = 1; index < dependencies.Count; index++)
        {
            var dependency = dependencies[index];
            value = dependency.MapValue(renderContext, value);
        }

        renderContext.CalculatedValues[Id] = value;
        return value;
    }

    public override object? MapValue(RenderContext renderContext, object parentValue)
    {
        return ValueMapper((TInput) parentValue, renderContext);
    }
    
    public void OnChange(Action<TReturn, IRenderContext> onChange)
    {
        SideEffects.Add(SideEffect.Create(onChange));
    }

    private List<IMappedValue> FindDependenciesRecursively(IMappedValue currentValue, List<IMappedValue> valuePath)
    {
        valuePath.Add(currentValue);
        var parent = currentValue.Parent; 
        if (parent == null)
        {
            valuePath.Reverse();
            return valuePath;
        }
        return FindDependenciesRecursively(parent, valuePath);
    }
}