using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EventManagement.Utility
{
    public class CandidateAuthorization : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string? userrole = context.HttpContext.Session.GetString("role");
            if(userrole != null)
            {
                if (userrole != "candidate")
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                    {
                        controller = "User",
                        action = "Login"
                    })) ;
                }
                
            }
            else
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "User",
                    action = "Login"
                }));
            }
            base.OnActionExecuting(context);
        }
    }
}
