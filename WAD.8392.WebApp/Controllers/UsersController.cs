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
                return NotFound();
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
            var id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
           
            var user = await _repository.GetByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            //modifying user account
            if (id != user.UserId)
            {
                return BadRequest();
            }
            //checking if all the fields are valid
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //checking if id of signed user is the same as id of user that is being modified (in other words, if the user is trying to modify his own account)
            if (!IsAuthorized(id))
            {
                ModelState.AddModelError("Authorization", "You are not the owner of this account");
                return Unauthorized(ModelState);
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
                
                if (CheckUniqueConstraintViolation(ex))
                {

                    ModelState.AddModelError("UserName", "This username is already taken");
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

                if (CheckUniqueConstraintViolation(ex))
                {
                    ModelState.AddModelError("UserName", "This username is already taken");
                    return BadRequest(ModelState);
                }
                throw;
            }

            return CreatedAtAction("GetUser", new { id = user.UserId }, user);

        }

        // DELETE: api/Users/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            //checking if user is trying to delete his own account (if id of logged user is the same of id of person that is being deleted )
            if (!IsAuthorized(id))
            {
                ModelState.AddModelError("Authorization", "You are not the owner of this account");
                return Unauthorized(ModelState);
            }
            var user = await _repository.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            await _repository.DeleteAsync(user);

            return NoContent();
        }

        private bool CheckUniqueConstraintViolation (Exception ex)
        {
            //if exception message contains the given words, it means that exception regarding unique username violation is caught
            if (ex.InnerException.Message.Contains("duplicate key row"))
            {
                return true;
            }
            return false;
        }

    }
}
