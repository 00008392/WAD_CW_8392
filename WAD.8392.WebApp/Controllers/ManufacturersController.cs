using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WAD._8392.DAL.Context;
using WAD._8392.DAL.DBO;
using WAD._8392.DAL.Repositories;

namespace WAD._8392.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManufacturersController : GenericController<Manufacturer>
    {
        //controller for handling manufacturers
        //note: methods of adding/updating/deleting manufacturers are not included in the client application,
        //since it would not make sense if any user registered in the system would be able to change this information
        //proper logic would be to allow admins to do that, that is why, role admin was put in authorize filter,
        //but it was decided not to develop role based authentication in the application for now,
        //maybe in the future it will be added 
        //that is why, database was already populated with sufficient number of manufacturers
        public ManufacturersController(IRepository<Manufacturer> repository):base(repository)
        {
        }

        // GET: api/Manufacturers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Manufacturer>>> GetManufacturers()
        {
            return await _repository.GetAllAsync();
        }

        // GET: api/Manufacturers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Manufacturer>> GetManufacturer(int id)
        {
            var manufacturer = await _repository.GetByIdAsync(id);

            if (manufacturer == null)
            {
                return NotFound();
            }

            return manufacturer;
        }

        // PUT: api/Manufacturers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles ="Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutManufacturer(int id, Manufacturer manufacturer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != manufacturer.ManufacturerId)
            {
                return BadRequest();
            }
            try
            {
                await _repository.UpdateAsync(manufacturer);
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

        // POST: api/Manufacturers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles ="Admin")]
        [HttpPost]
        public async Task<ActionResult<Manufacturer>> PostManufacturer(Manufacturer manufacturer)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _repository.AddAsync(manufacturer);

            return CreatedAtAction("GetManufacturer", new { id = manufacturer.ManufacturerId }, manufacturer);
        }

        // DELETE: api/Manufacturers/5
        [Authorize(Roles ="Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteManufacturer(int id)
        {
            var manufacturer = await _repository.GetByIdAsync(id);
            if (manufacturer == null)
            {
                return NotFound();
            }

            await _repository.DeleteAsync(manufacturer);

            return NoContent();
        }

     
    }
}
