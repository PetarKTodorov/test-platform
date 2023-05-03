namespace TestPlatform.Application.Infrastructures.Filtres
{
    using System;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class PreventAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.HttpContext.User.Claims.Any())
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
