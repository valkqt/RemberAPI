using ApplicationCore.Interfaces.Services;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RemberAPI.Controllers
{
    [Route("api/Users/{userId}/[controller]")]
    [ApiController]
    public class DecksController : ControllerBase
    {
        private readonly IDecksService _decksService;
        public DecksController(IDecksService decksService)
        {
            _decksService = decksService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int userId)
        {
            IEnumerable<Deck> userDecks = await _decksService.ListUserDecks(userId);

            return Ok(userDecks);
        }

    }
}
