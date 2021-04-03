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
    //parent controller needed to extract repetitive code
    public class GenericController<T> : ControllerBase where T : class
    {
        protected readonly IRepository<T> _repository;
        //repetitive code
        protected GenericController(IRepository<T> repository)
        {
            _repository = repository;
        }
        //check used in methods of modifying/deleting user/product
        //if user is authorized (has JWT), but id of user or product that this user is trying to change is different,
        //then user is trying to change another account
        protected bool IsAuthorized(int id)
        {
            //getting the id of logged user
            var loggedUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            //comparing it with id of entity that user is trying to change
            return loggedUserId == id;
            
        }
    }
}
