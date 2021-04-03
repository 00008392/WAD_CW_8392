using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WAD._8392.WebApp.DTO
{
    //DTO for returning user to client without exposing password
    public class UserDetails
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime DateRegistered { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

    }
}
