using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Interfaces.Repositories;
using ApplicationCore.Interfaces.Services;


namespace ApplicationCore.Services
{
    public class CardsService : ICardsService
    {
        private readonly ICardsRepository _cardsRepository;
        public CardsService(ICardsRepository cardsRepository)
        {
            _cardsRepository = cardsRepository;
        }

        public async Task<IEnumerable<Card>> GetCards()
        {

            return await _cardsRepository.ListAsync();
        }
    }
}
