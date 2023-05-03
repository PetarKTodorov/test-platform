namespace TestPlatform.Application.Infrastructures.Filtres
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    using TestPlatform.Application.Infrastructures.Helpers;

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CustomAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private IEnumerable<string> AllowedRoleNames { get; set; }

        public CustomAuthorizeAttribute(params string[] allowedRoles)
        {
            this.AllowedRoleNames = allowedRoles;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var userClaims = context.HttpContext.User.Claims;
            if (!userClaims.Any())
            {
                context.Result = new NotFoundResult();
                return;
            }

            var roles = context.HttpContext.User.FindAll(UserClaimTypes.ROLE)
                .Select(r => r.Value)
                .Where(r => this.AllowedRoleNames.Contains(r));

            if (!roles.Any())
            {
                context.Result = new NotFoundResult();
                return;
            }
        }
    }
}
