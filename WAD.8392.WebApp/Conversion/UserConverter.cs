using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WAD._8392.DAL.DBO;
using WAD._8392.DAL.Repositories;
using WAD._8392.WebApp.DTO;

namespace WAD._8392.WebApp.Conversion
{
    public class UserConverter : IConverter<User, UserDetails>
    {
        //concrete converter
        //converting user to UserDetails to hide password 
        public UserDetails ConvertToDTO(User obj)
        {
            var result = new UserDetails
            {
                UserId = obj.UserId,
                FirstName = obj.FirstName,
                LastName = obj.LastName,
                DateOfBirth = obj.DateOfBirth,
                DateRegistered = obj.DateRegistered,
                UserName = obj.UserName,
                PhoneNumber = obj.PhoneNumber,
                Email = obj.Email
            };
            return result;
        }
    }
}
