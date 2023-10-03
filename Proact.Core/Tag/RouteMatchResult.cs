namespace Proact.Core.Tag;

public class RouteMatchResult
{
    public Route Route { get; }
    public bool IsMatched { get; set; }
    public Dictionary<string, string> PathParameters { get; set; }
    
    public RouteMatchResult(Route route, bool isMatched) : this(route, isMatched, new Dictionary<string, string>())
    {
    }
    
    public RouteMatchResult(Route route, bool isMatched, Dictionary<string, string> pathParameters)
    {
        Route = route;
        IsMatched = isMatched;
        PathParameters = pathParameters;
    }
    
    
}