using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ApplicationCore.Interfaces.Services;

namespace ApplicationCore.Services
{
    public class AuthCookieService : IAuthCookieService
    {
        public CookieOptions CreateAuthCookie(TimeSpan timespan)
        {

            var cookieOptions = new CookieOptions()
            {
                IsEssential = true,
                Expires = (DateTime.UtcNow + timespan),
                Secure = true,
                HttpOnly = true,
                SameSite = SameSiteMode.None,

            };

            return cookieOptions;
        }


    }
}
