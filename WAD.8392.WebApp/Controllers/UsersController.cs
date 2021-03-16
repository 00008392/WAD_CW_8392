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

namespace WAD._8392.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : GenericController<User>
    {
        
        public UsersController(IRepository<User> repository):base(repository)
        {
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _repository.GetAllAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            
            var user = await _repository.GetByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.UserId)
            {
                return BadRequest();
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
           
            
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _repository.AddAsync(user);

            return CreatedAtAction("GetUser", new { id = user.UserId }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
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
