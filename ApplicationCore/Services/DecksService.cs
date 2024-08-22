using ApplicationCore.Interfaces.Repositories;
using ApplicationCore.Interfaces.Services;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class DecksService : IDecksService
    {
        private readonly IDecksRepository _decksRepository;
        public DecksService(IDecksRepository decksRepository)
        {
            _decksRepository = decksRepository;
        }

        public async Task<IEnumerable<Deck>> ListUserDecks(int userId)
        {
            return await _decksRepository.GetUserDecks(userId);
        }

    }
}
