
using Microsoft.AspNetCore.Mvc.Filters;
using z8l_intranet_be.Helper.Exception;
using z8l_intranet_be.Infrastructure.Schemas;
namespace z8l_intranet_be.Helpers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = (UserSchema)context.HttpContext.Items["User"];
            if (user == null)
            {
                throw new UnauthorizedException();
            }
        }
    }
}
