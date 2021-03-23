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

namespace WAD._8392.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : GenericController<User>
    {
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

            return Ok(_converter.ConvertToDTO(user));
        }

        [Authorize]
        [HttpGet("Account")]
        public async Task<ActionResult<User>> GetLoggedUser()
        {
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
            if (id != user.UserId)
            {
                return BadRequest();
            }
            if (!IsAuthorized(id))
            {
                ModelState.AddModelError("Authoriation", "You should login into this account to modify it");
                return Unauthorized(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
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

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            var users = await _repository.GetAllAsync();
            var u = users.FirstOrDefault(u => u.UserName.ToLower().Equals(user.UserName.ToLower()));
            if(u!=null)
            {
                ModelState.AddModelError(user.UserName, "This username is already taken");
            }

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            user.DateRegistered = DateTime.Now;
               await _repository.AddAsync(user);

            return CreatedAtAction("GetUser", new { id = user.UserId }, user);
        }

        // DELETE: api/Users/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (!IsAuthorized(id))
            {
                ModelState.AddModelError("Authoriation", "You should login into this account to delete it");
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


    }
}
