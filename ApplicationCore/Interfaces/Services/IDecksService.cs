using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces.Services
{
    public interface IDecksService
    {
        public Task<IEnumerable<Deck>> ListUserDecks(int userId);

    }
}
