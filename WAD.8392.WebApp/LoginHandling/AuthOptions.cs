using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WAD._8392.WebApp.LoginHandling
{
    
    public class AuthOptions
    {
        //secret key to create JWT token signature
        const string KEY = "This is a key for authorization handling"; 
        public static byte[] TokenKey()
        {
            //encoded key
            return Encoding.ASCII.GetBytes(KEY);

        }
    }
}
