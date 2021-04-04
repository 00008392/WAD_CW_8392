using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using WAD._8392.DAL.Context;
using WAD._8392.DAL.DBO;
using WAD._8392.WebApp.DTO;
using WAD._8392.DAL.Repositories;
using WAD._8392.WebApp.Conversion;
using System.Security.Claims;
using Microsoft.Data.SqlClient;

namespace WAD._8392.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : GenericController<User>
    {
        //controller for handling users
        private readonly IConverter<User, UserDetails> _converter;
        
        public UsersController(IRepository<User> repository, IConverter<User, UserDetails> converter) :base(repository)
        {
            _converter = converter;
        }
        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _repository.GetAllAsync();
            //getting all users, but without password information (DTO is used)
            var dtoUsers = users.Select(user => _converter.ConvertToDTO(user)).ToList();
            return Ok(dtoUsers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {

            var user = await _repository.GetByIdAsync(id);

            if (user == null)
            {
                return NotFound("User is not found");
            }
            //get used details without password information (DTO is used)
            return Ok(_converter.ConvertToDTO(user));
        }

        [Authorize]
        [HttpGet("Account")]
        public async Task<ActionResult<User>> GetLoggedUser()
        {
            //getting information about authenticated user (this info can be accessed only by signed in user)
            //getting signed in user id from jwt (unique identifier)
            var id = GetLoggedUserId();
            var user = await _repository.GetByIdAsync(id);

            if (user == null)
            {
                return NotFound("User does not exist");
            }

            return user;
        }

 
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPut]
        //in this method, id is not passed through url, instead it is taken from JWT token signature (name identifier)
        //since only logged in user can modify his own account
        //JWT token is passed in request header
        public async Task<IActionResult> PutUser(User user)
        {
            //getting id of user from JWT signature
            int id = GetLoggedUserId();
            //checking if all the fields are valid
            //this check is placed before id check in order to avoid exception if user is null (since automatic model validation is disabled)
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //checking if user is modifying his own account
            if (id != user.UserId)
            {
                return Unauthorized("You are not the owner of this account");
            }

            try
            {
                await _repository.UpdateAsync(user);
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!_repository.IfExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            //catching exception of unique constraint violation (when user is trying to change username to the one that already exists in the database)
            catch (Exception ex)
            {

                if (CheckUniqueUsername(ex)!=null)
                {
                    ModelState.AddModelError("UserName", CheckUniqueUsername(ex));
                    return BadRequest(ModelState);
                }
                throw;
            }

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            //method for registering users
            //checking if all fields are valid
            if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
            //date registered is automatically set when user is created
            user.DateRegistered = DateTime.Now;
            try
            {
                await _repository.AddAsync(user);

            }
            //catching exception of unique constraint violation (when user is trying to register with username that already exists in the database)
            catch (Exception ex)
            {

                if (CheckUniqueUsername(ex)!=null)
                {
                    ModelState.AddModelError("UserName", CheckUniqueUsername(ex));
                    return BadRequest(ModelState);
                }
                throw;
            }

            return CreatedAtAction("GetUser", new { id = user.UserId }, user);

        }

        // DELETE: api/Users/5
        [Authorize]
        [HttpDelete]
        //in this method, id is not passed through url, instead it is taken from JWT token signature (name identifier)
        //since only logged in user can delete his own account
        //JWT token is passed in request header
        public async Task<IActionResult> DeleteUser()
        {
            //getting id of user from JWT signature
            int id = GetLoggedUserId();
            //checking if user exists
            var user = await _repository.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound("User does not exist");
            }
            await _repository.DeleteAsync(user);

            return NoContent();
        }

        private string CheckUniqueUsername(Exception ex)
        {
            if(CheckInnerException(ex, "duplicate key row"))
            {
                return "This username is already taken.";
            }
            return null;
        }

    }
}
