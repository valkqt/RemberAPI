using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Authorization.AuthorizeJwtPolicy
{
    public class AuthorizeJwtHandler : AuthorizationHandler<AuthorizeJwtRequirement>, IAuthorizationHandler
    {
        IHttpContextAccessor _httpContextAccessor;
        IDataProtector _dataProtector;
        public AuthorizeJwtHandler(IHttpContextAccessor httpContextAccessor, IDataProtectionProvider dataProtector)
        {
            _httpContextAccessor = httpContextAccessor;
            _dataProtector = dataProtector.CreateProtector("CookieProtector");
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AuthorizeJwtRequirement requirement)
        {
            HttpContext? httpContext = _httpContextAccessor.HttpContext;

            string? cookie = httpContext?.Request.Cookies["accessToken"];

            if (cookie != null)
            {
                string accessToken = _dataProtector.Unprotect(cookie);

                JwtSecurityToken securityToken;

                try
                {

                    securityToken = new JwtSecurityToken(accessToken);

                    if (securityToken.ValidTo > DateTime.UtcNow)
                    {
                        context.Succeed(requirement);
                    };

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
