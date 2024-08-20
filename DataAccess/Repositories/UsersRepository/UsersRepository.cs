using ApplicationCore.Interfaces.Repositories;
using ApplicationCore.Models;
using ApplicationCore.Models.User;
using ApplicationCore.Requests;
using ApplicationCore.Utility;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace DataAccess.Repositories.UsersRepository
{
    public class UsersRepository : IUsersRepository
    {
        private readonly IConfiguration _configuration;
        public UsersRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<User>? GetById(int id)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("RemberConn")))
            {

                object parameters = new { @ID = id };
                string sql = $"SELECT Id, Username, Email, PasswordHash, EmailVerified, CreatedAt FROM Users WHERE Id = @ID";

                return await connection.QuerySingleAsync<User>(sql, parameters);
            }
        }

        public async Task<BaseUser> GetBaseUserById(int id)
        {

            using (var connection = new SqlConnection(_configuration.GetConnectionString("RemberConn")))
            {

                object parameters = new { @ID = id };
                string sql = $"SELECT Id, Username, Email, CreatedAt FROM Users WHERE Id = @ID";

                return await connection.QuerySingleAsync<BaseUser>(sql, parameters);
            }


        }

        public async Task<User> GetByUsername(string username)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("RemberConn")))
            {

                object parameters = new { @USERNAME = username };
                string sql = $"SELECT Id, Username, Email, PasswordHash, EmailVerified, CreatedAt FROM Users WHERE Username = @USERNAME";


                return await connection.QuerySingleAsync<User>(sql, parameters); ;
            }
        }

        public async Task CreateUser(PostRegisterModel newUser)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("RemberConn")))
            {
                object parameters = new
                {
                    @USERNAME = newUser.Username,
                    @PASSWORD = newUser.Password,
                    @EMAIL = newUser.Email,
                };
                string sql = $"INSERT INTO Users (Username, PasswordHash, Email) VALUES (@USERNAME, @PASSWORD, @EMAIL)";

                await connection.ExecuteAsync(sql, parameters);

            }

        }

        public async Task<IEnumerable<Card>> GetUserCards(int id)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("RemberConn")))
            {

                object parameters = new
                {
                    @USERID = id
                };
                string sql = $"SELECT * FROM Cards WHERE UserId = @USERID";

                return await connection.QueryAsync<Card>(sql, parameters);
            }
        }
    }
}
