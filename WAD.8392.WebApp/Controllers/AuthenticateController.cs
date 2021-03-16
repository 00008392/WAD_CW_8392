﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WAD._8392.WebApp.DTO;
using WAD._8392.WebApp.LoginHandling;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WAD._8392.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IAuthenticationManager _authenticationManager;

        public AuthenticateController(IAuthenticationManager authenticationManager)
        {
            _authenticationManager = authenticationManager;
        }

        // POST api/<AuthenticateController>
        [HttpPost]
        public async Task<IActionResult> Authenticate([FromBody] LoginDto login)
        {
            var token = await _authenticationManager.Authenticate(login.UserName, login.Password);

            if (token == null)
                return Unauthorized();

            return Ok(token);
        }

    }
}