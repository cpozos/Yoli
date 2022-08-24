using Microsoft.AspNetCore.Mvc.Filters;

namespace Yoli.WebApi.Filters
{
    public class RequestLogger : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            throw new NotImplementedException();
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            throw new NotImplementedException();
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            OnActionExecuting(context);
            var result = await next();
            OnActionExecuted(result);
        }
    }
}
