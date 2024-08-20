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

namespace DataAccess.Repositories.CardsRepository
{
    public class CardsRepository : ICardsRepository
    {
        private readonly IConfiguration _configuration;
        public CardsRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<IEnumerable<Card>> ListAsync()
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("RemberConn")))
            {
                string sql = "SELECT c.Id, c.Front, c.Back, c.UserId, u.Id, u.Username, u.Email, u.CreatedAt FROM Cards c INNER JOIN Users u ON c.UserId = u.Id;";
                IEnumerable<Card> cards = await connection.QueryAsync<Card, BaseUser, Card>(sql, (card, user) => { card.User = user; return card; }, splitOn: "Id");

                return cards;
            }

        }
    }
}
