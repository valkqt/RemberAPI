using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Models;
using ApplicationCore.Services;
using ApplicationCore.Models.User;
using ApplicationCore.Interfaces.Repositories;
using DataAccess.Contexts;

namespace DataAccess.Repositories.CardsRepository
{
    public class CardsRepository : ICardsRepository
    {
        private readonly IConfiguration _configuration;
        private readonly RemberDbContext _context;
        public CardsRepository(IConfiguration configuration, RemberDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }
        public async Task<IEnumerable<Card>> ListAsync()
        {
            using (var connection = _context.Connect())
            {
                string sql = "SELECT c.Id, c.Front, c.Back, c.UserId, d.Id, d.Name, FROM Cards c INNER JOIN Decks d ON c.DeckId = d.Id;";
                IEnumerable<Card> cards = await connection.QueryAsync<Card, Deck, Card>(sql, (card, deck) => { card.Deck = deck; return card; }, splitOn: "Id");

                return cards;
            }

        }
    }
}
