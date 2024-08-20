using ApplicationCore.Interfaces.Services;
using ApplicationCore.Models.User;
using ApplicationCore.Services;
using ApplicationCore.Utility;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace RemberAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IJWTAuthService _jWTAuthService;
        private readonly IAuthCookieService _cookieService;
        private readonly IDataProtector _dataProtector;
        public AuthController(IJWTAuthService jWTAuthService, IDataProtectionProvider dataProtectionProvider, IAuthCookieService cookieService)
        {
            _jWTAuthService = jWTAuthService;
            _dataProtector = dataProtectionProvider.CreateProtector("CookieProtector");
            _cookieService = cookieService;
        }

        [HttpPost("Signin")]
        public async Task<IActionResult> Authenticate([FromBody] PostLoginModel request)
        {
            try
            {
                BaseUser user = await _jWTAuthService.Authenticate(request);

                var claims = new List<Claim>() {
                new Claim("Id", user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

                string accessToken = _dataProtector.Protect(_jWTAuthService.GenerateAccessToken(claims));
                string? refreshToken = null;

                CookieOptions accessCookieOptions = _cookieService.CreateAuthCookie(TimeSpan.FromMinutes(1));
                Response.Cookies.Append("accessToken", accessToken, accessCookieOptions);

                if (request.Persist)
                {
                    refreshToken = _dataProtector.Protect(_jWTAuthService.GenerateRefreshToken());
                    CookieOptions refreshCookieOptions = _cookieService.CreateAuthCookie(TimeSpan.FromMinutes(5));
                    Response.Cookies.Append("refreshToken", refreshToken, refreshCookieOptions);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }
    }
}
