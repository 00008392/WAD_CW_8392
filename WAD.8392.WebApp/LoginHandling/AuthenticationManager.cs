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
    //service for handling JWT tokens
    public class AuthenticationManager : IAuthenticationManager
    {

        private readonly IRepository<User> _repository;

        public AuthenticationManager(IRepository<User> repository)
        {
            _repository = repository;

        }
        //method for creating web token
        public async Task<string> Authenticate(string username, string password)
        {
            var user = await CheckCredentials(username, password);
            if(user==null)
            {
                return null;
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = AuthOptions.TokenKey();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    //setting username and userId as unique identifier of current user
                    //the unique identifier will be later used in controller methods with [authorize] filter to retrieve id of autheticated user
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString())
                }),
                //token is valid within 1 hour
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        private async Task<User> CheckCredentials(string username, string password)
        {
            List<User> users = await _repository.GetAllAsync();
            //check user credentials
            if (!users.Any(u => u.UserName == username && u.Password == password))
            {
                return null;
            }
            //if exists, find user
            var user = users.FirstOrDefault(u => u.UserName == username && u.Password == password);
            return user;
        }
    }
}
