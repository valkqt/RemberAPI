using ApplicationCore.Authorization;
using ApplicationCore.Authorization.AuthorizeJwtPolicy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Authorization.AuthorizeUserPolicy
{
    public class AuthorizeUserHandler : AuthorizationHandler<AuthorizeUserRequirement>, IAuthorizationHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IDataProtector _dataProtector;
        public AuthorizeUserHandler(IHttpContextAccessor httpContextAccessor, IDataProtectionProvider dataProtector)
        {
            _httpContextAccessor = httpContextAccessor;
            _dataProtector = dataProtector.CreateProtector("CookieProtector");
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AuthorizeUserRequirement requirement)
        {

            HttpContext? httpContext = _httpContextAccessor.HttpContext;

            string? cookie = httpContext?.Request.Cookies["accessToken"];

            if (httpContext == null)
            {
                context.Fail();
                return Task.FromResult(0);

            }

            if (cookie != null)
            {
                string accessToken = _dataProtector.Unprotect(cookie);

                JwtSecurityToken securityToken;

                try
                {
                    securityToken = new JwtSecurityToken(accessToken);
                    if (securityToken.Subject == httpContext?.GetRouteValue("id")?.ToString())
                    {
                        context.Succeed(requirement);
                    }


                }
                catch (Exception)
                {
                    context.Fail();
                }

            }

            return Task.FromResult(0);





        }
    }
}