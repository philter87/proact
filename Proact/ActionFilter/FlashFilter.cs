
using Microsoft.AspNetCore.Mvc.Filters;

namespace Proact.ActionFilter;

public class FlashFilter : IActionFilter
{
    private readonly FlashService _flashService;

    public FlashFilter(FlashService flashService)
    {
        _flashService = flashService;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        // The context is forwarded to the flashService which is a singleton an able to cache results
        _flashService.OnActionExecuting(context);
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        // The context is forwarded to the flashService which is a singleton an able to cache results
        _flashService.OnActionExecuted(context);
    }

    
}