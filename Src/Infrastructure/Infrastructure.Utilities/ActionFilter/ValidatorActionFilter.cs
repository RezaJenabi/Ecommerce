using System.Linq;
using Infrastructure.Utilities.Enums;
using Infrastructure.Utilities.FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Infrastructure.Utilities.ActionFilter
{
    public class ValidatorActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new JsonResult(new ErrorFluentValidation
                {
                    Errors = context.ModelState.Values.Where(v => v.Errors.Count > 0)
                        .SelectMany(v => v.Errors)
                        .Select(v => v.ErrorMessage)
                        .ToList()
                })
                {
                    StatusCode = (int)CustomHttpStatusCode.FluentValidation,
                };
            }
        }
    }
}
