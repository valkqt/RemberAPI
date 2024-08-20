using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApplicationCore.Models;
using ApplicationCore.Interfaces.Services;

namespace RemberAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        private readonly ICardsService _cardsService;
        public CardsController(ICardsService cardsService)
        {

            _cardsService = cardsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCardsAsync()
        {
            IEnumerable<Card> cards = await _cardsService.GetCards();

            return Ok(cards);
        }

    }
}
