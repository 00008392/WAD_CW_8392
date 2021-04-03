using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WAD._8392.WebApp.LoginHandling
{
    //interface for class that handles JWT tokens
   public interface IAuthenticationManager
    {
        Task<string> Authenticate(string username, string password);
    }
}
