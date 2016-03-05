using System;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Filters;

namespace Toolbox.WebApi.Filters
{
    public class ValidateModelStateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if ( !context.ModelState.IsValid )
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }
    }
}
