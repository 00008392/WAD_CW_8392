using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WAD._8392.DAL.DBO;
using WAD._8392.DAL.Repositories;

namespace WAD._8392.WebApp.LoginHandling
{
    public class AuthenticationManager : IAuthenticationManager
    {
        private readonly IRepository<User> _repository;

        public AuthenticationManager(IRepository<User> repository)
        {
            _repository = repository;

        }
        public async Task<string> Authenticate(string username, string password)
        {
            List<User> users = await _repository.GetAllAsync();
            if (!users.Any(u => u.UserName == username && u.Password == password))
            {
                return null;
            }
            var user = users.FirstOrDefault(u => u.UserName == username && u.Password == password);
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = AuthOptions.TokenKey();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
