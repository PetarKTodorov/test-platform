namespace TestPlatform.Application.Infrastructures.Filtres
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using TestPlatform.Application.Infrastructures.ApplicationUser;

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
            var isAuthenticated = context.HttpContext.User.Identity == null
                || context.HttpContext.User.Identity.IsAuthenticated;

            if (!isAuthenticated)
            {
                context.Result = new NotFoundResult();
                return;
            }

            // Protect route just for the user regardless of the role
            if (!this.AllowedRoleNames.Any())
            {
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
