using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Models;

namespace ApplicationCore.Interfaces.Services
{
    public interface ICardsService
    {
        public Task<IEnumerable<Card>> GetCards();
    }
}
