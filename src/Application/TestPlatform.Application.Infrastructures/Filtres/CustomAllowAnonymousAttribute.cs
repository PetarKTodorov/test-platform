﻿namespace TestPlatform.Application.Infrastructures.Filtres
{
    using System;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CustomAllowAnonymousAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.HttpContext.User.Identity == null
                || context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new NotFoundResult();
            }
        }
    }
}
