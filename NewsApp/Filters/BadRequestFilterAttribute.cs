using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Web.Http.Results;
using BadRequestResult = Microsoft.AspNetCore.Mvc.BadRequestResult;

namespace NewsApp.Filters
{
    public class BadRequestFilterAttribute : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var result = await next();
            if ((result.Result as BadRequestResult)?.StatusCode == 400)
            {
                result.Result = new ViewResult
                {

                    ViewName = "Views/Articles/Error.cshtml"
                };
            }
        }
    }
}
