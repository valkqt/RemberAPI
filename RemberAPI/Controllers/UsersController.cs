using ApplicationCore.Interfaces.Repositories;
using ApplicationCore.Interfaces.Services;
using ApplicationCore.Models;
using ApplicationCore.Models.User;
using ApplicationCore.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.DataProtection;

namespace RemberAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "AuthorizeJWT")]

    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;
        private readonly IDataProtector _dataProtector;
        public UsersController(IUsersService usersService, IDataProtectionProvider dataProtectionProvider)
        {

            _usersService = usersService;
            _dataProtector = dataProtectionProvider.CreateProtector("CookieProtector");

        }

        [HttpGet("{id}")]
        [Authorize(Policy = "AuthorizeUser")]
        public async Task<IActionResult> GetById(int id)
        {
            BaseUser? user = await _usersService.GetById(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpGet]
        public async Task<IActionResult> asdf()
        {

            BaseUser? user = await _usersService.GetById(2);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PostRegisterModel newUser)
        {

            try
            {
                await _usersService.CreateUser(newUser);
                return Created();
            }
            catch
            {
                return Conflict();
            }

        }

        [HttpGet("{id}/Cards")]
        public async Task<IActionResult> GetUserCards(int id)
        {
            try
            {
                IEnumerable<Card> cards = await _usersService.GetUserCards(id);
                return Ok(cards);

            }
            catch
            {
                return Unauthorized("plofi :(");
            }
        }
    }
}
