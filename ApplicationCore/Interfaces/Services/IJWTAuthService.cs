using ApplicationCore.Models.User;
using ApplicationCore.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces.Services
{
    public interface IJWTAuthService
    {
        public Task<BaseUser> Authenticate(PostLoginModel user);
        public string GenerateAccessToken(IEnumerable<Claim> Claims);
        public string GenerateRefreshToken();
        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
