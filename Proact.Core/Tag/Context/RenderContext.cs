using System.Text;
using Proact.Core.Value;

namespace Proact.Core.Tag.Context;

public class RenderContext : IRenderContext
{
    public IServiceProvider ServiceProvider { get; set; }
    public string CurrentUrlPath { get; set; }
    public string CurrentUrlPattern { get; set; }
    public string NextUrlPath { get; set; }
    public Dictionary<string, List<string>> QueryParameters { get; set; }
    public Dictionary<string, string> PathParameters { get; set; }
    public Dictionary<string, ValueChangeCommand> ValueChanges { get; set; }
    public Dictionary<string, object> CalculatedValues { get; set; } = new();
    public List<string> ExecutedSideEffects { get; set; } = new();
    public List<ValueChangeCommand> ServerValueChanges { get; set; } = new();

    public RenderContext(IServiceProvider serviceProvider, string currentUrlPath, Dictionary<string, ValueChangeCommand>? values = null)
    {
        ServiceProvider = serviceProvider;
        CurrentUrlPath = currentUrlPath;
        CurrentUrlPattern = currentUrlPath;
        PathParameters = new Dictionary<string, string>();
        QueryParameters = RenderContextUtils.GetQueryParameters(currentUrlPath);
        ValueChanges = values ?? new Dictionary<string, ValueChangeCommand>();
    }

    public void Navigate(string relativeUrl, Dictionary<string, string> queryParameters)
    {
        ServerValueChanges.Add(new ValueChangeCommand(Constants.RouteUrlValueId, relativeUrl));
        
        var urlBuilder = new StringBuilder(relativeUrl);
        var qpList = queryParameters.ToList();
        
        for (var index = 0; index < qpList.Count; index++)
        {
            var qp = qpList[index];
            ServerValueChanges.Add(new ValueChangeCommand(qp.Key, qp.Value));
            urlBuilder.Append(index == 0 ? "?" : "&");
            urlBuilder.Append($"{qp.Key}={qp.Value}");
        }
        NextUrlPath = urlBuilder.ToString();
    }

    public S? GetService<S>() where S: class
    {
        return (S?) ServiceProvider.GetService(typeof(S));
    }

    public void TriggerValueChange<T>(RootValue<T> value, T newValue)
    {
        ServerValueChanges.Add(new ValueChangeCommand(value.Id, Json.AsString(newValue)));
    }
}