using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces.Repositories
{
    public interface IDecksRepository
    {
        public Task<IEnumerable<Deck>> GetUserDecks(int userId);

    }
}
