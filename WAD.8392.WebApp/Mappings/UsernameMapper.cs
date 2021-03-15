using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WAD._8392.DAL.DBO;
using WAD._8392.DAL.Repositories;

namespace WAD._8392.WebApp.Mappings
{
    public class UsernameMapper
    {
        private readonly IRepository<User> _repository;
        public UsernameMapper(IRepository<User> repository)
        {
            _repository = repository;
        }
        public async Task<User> MapUsernameToUser(string username)
        {
            var users = await _repository.GetAllAsync();
            var user = users.FirstOrDefault(user => user.UserName.Equals(username));
            return user;
        }
    }
}
