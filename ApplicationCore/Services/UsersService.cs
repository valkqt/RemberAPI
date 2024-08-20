using ApplicationCore.Interfaces.Repositories;
using ApplicationCore.Interfaces.Services;
using ApplicationCore.Models;
using ApplicationCore.Models.User;
using ApplicationCore.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace ApplicationCore.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;
        public UsersService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }
        public async Task<BaseUser> GetById(int id)
        {
            return await _usersRepository.GetBaseUserById(id);
        }

        public async Task CreateUser(PostRegisterModel newUser)
        {
            newUser.Password = Crypto.HashPassword(newUser.Password);
            await _usersRepository.CreateUser(newUser);

        }

        public async Task<IEnumerable<Card>> GetUserCards(int id)
        {
            return await _usersRepository.GetUserCards(id);
        }

    }
}
