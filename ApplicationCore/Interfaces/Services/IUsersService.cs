using ApplicationCore.Models;
using ApplicationCore.Models.User;
using ApplicationCore.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces.Services
{
    public interface IUsersService
    {
        public Task<BaseUser> GetById(int id);
        public Task CreateUser(PostRegisterModel newUser);
        public Task<IEnumerable<Card>> GetUserCards(int id);
    }
}
