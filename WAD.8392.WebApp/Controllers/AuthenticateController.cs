using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WAD._8392.WebApp.LoginHandling;


namespace WAD._8392.WebApp.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    //controller that handles authentication
    public class AuthenticateController : ControllerBase
    {
        //service for generating JWT
        private readonly IAuthenticationManager _authenticationManager;

        public AuthenticateController(IAuthenticationManager authenticationManager)
        {
            _authenticationManager = authenticationManager;
        }
        //api endpoint for authentication
        [HttpPost]
        public async Task<IActionResult> Authenticate([FromBody] Login login)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //getting token based on credentials
            var token = await _authenticationManager.Authenticate(login.UserName, login.Password);
            //if token is null, it means that credentials are not correct
            if (token == null)
                return Unauthorized();
            //if credentials are correct, user can use returned JWT to access authorized api endpoints
            return Ok(token);
        }

    }
}
