using ApplicationCore.Models.User;
using ApplicationCore.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.IdentityModel.Tokens;
using Microsoft.IdentityModel.Tokens;
using System.Web.Helpers;
using ApplicationCore.Interfaces.Repositories;
using ApplicationCore.Interfaces.Services;



namespace ApplicationCore.Services
{
    public class JWTAuthService : IJWTAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IUsersRepository _usersRepository;
        public JWTAuthService(IConfiguration configuration, IUsersRepository usersRepository)
        {
            _configuration = configuration;
            _usersRepository = usersRepository;
        }

        public async Task<BaseUser> Authenticate(PostLoginModel request)
        {
            User userInfo = await _usersRepository.GetByUsername(request.Username);
            bool passwordMatch = Crypto.VerifyHashedPassword(userInfo.PasswordHash, request.Password);

            if (passwordMatch)
            {
                BaseUser user = new BaseUser(userInfo.Id, userInfo.Username, userInfo.Email, userInfo.CreatedAt);

                return user;
            }
            else
            {
                throw new Exception("wehhhh error");
            }

        }
        public string GenerateAccessToken(IEnumerable<Claim> claims)
        {
            string issuer = _configuration["Jwt:Issuer"]!;
            string audience = _configuration["Jwt:Audience"]!;
            byte[] key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]!);

            var signinCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha512Signature);
            var tokenOptions = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.Now.AddSeconds(15),
                signingCredentials: signinCredentials
            );

            string tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return tokenString;

        }
        public string GenerateRefreshToken()
        {
            return Guid.NewGuid().ToString();

        }
        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {

            byte[] key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]!);

            var validationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateLifetime = false
            };


            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            ClaimsPrincipal? principal = tokenHandler.ValidateToken(token, validationParameters, out securityToken);

            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha512Signature, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }

    }
}
