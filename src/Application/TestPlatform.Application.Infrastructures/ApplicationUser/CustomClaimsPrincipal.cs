namespace TestPlatform.Application.Infrastructures.ApplicationUser
{
    using System.Security.Claims;
    using System.Security.Principal;

    public class CustomClaimsPrincipal : ClaimsPrincipal
    {
        public CustomClaimsPrincipal(IPrincipal principal)
            : base(principal)
        {

        }

        public override bool IsInRole(string role)
        {
            base.IsInRole(role);

            var userRoles = this.Claims.Where(c => c.Type == UserClaimTypes.ROLE)
                .Select(r => r.Value);

            bool isInRole = !userRoles.Any();

            if (isInRole)
            {
                return false;
            }

            isInRole = userRoles.Any(c => c == role);

            return isInRole;
        }
    }
}
