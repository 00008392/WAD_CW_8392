using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WAD._8392.DAL.Repositories;

namespace WAD._8392.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenericController<T> : ControllerBase where T : class
    {
        protected readonly IRepository<T> _repository;
        protected GenericController(IRepository<T> repository)
        {
            _repository = repository;
        }
        protected bool IsAuthorized(int id)
        {
            var loggedUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            return loggedUserId == id;
            
        }
    }
}
