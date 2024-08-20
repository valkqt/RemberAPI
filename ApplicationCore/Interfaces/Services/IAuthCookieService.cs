using ApplicationCore.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces.Services
{
    public interface IAuthCookieService
    {
        public CookieOptions CreateAuthCookie(TimeSpan timespan);
    }
}
