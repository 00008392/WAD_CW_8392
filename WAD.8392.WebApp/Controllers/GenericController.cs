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
        //getting id of logged user from JWT signature
        protected int GetLoggedUserId()
        {
            return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }
        //this method checks for specific exception based on exception message 
        protected bool CheckInnerException(Exception ex, string message)
        {
            if(ex.InnerException.Message.Contains(message))
            {
                return true;
            }
            return false;
        }

    }
}
