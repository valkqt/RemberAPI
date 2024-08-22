using ApplicationCore.Interfaces.Repositories;
using ApplicationCore.Models;
using ApplicationCore.Models.User;
using Dapper;
using DataAccess.Contexts;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.DecksRepository
{
    public class DecksRepository : IDecksRepository
    {
        private readonly IConfiguration _configuration;
        private readonly RemberDbContext _context;
        public DecksRepository(IConfiguration configuration, RemberDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public async Task<IEnumerable<Deck>> GetUserDecks(int userId)
        {
            using (var connection = _context.Connect())
            {
                string sql = "SELECT Id, Name, UserId, IsPrivate, Scope FROM Decks";
                IEnumerable<Deck> decks = await connection.QueryAsync<Deck>(sql);

                return decks;

            }

        }

    }
}
