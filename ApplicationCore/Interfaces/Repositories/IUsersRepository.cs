using ApplicationCore.Models;
using ApplicationCore.Models.User;
using ApplicationCore.Requests;
using ApplicationCore.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces.Repositories
{
    public interface IUsersRepository
    {
        public Task<User>? GetById(int id);
        public Task<User> GetByUsername(string username);
        public Task<BaseUser> GetBaseUserById(int id);
        public Task CreateUser(PostRegisterModel newUser);
        public Task<IEnumerable<Card>> GetUserCards(int id);
    }
}
