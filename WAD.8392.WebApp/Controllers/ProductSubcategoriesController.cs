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
    [Route("api/subcategories")]
    [ApiController]
    public class ProductSubcategoriesController : GenericController<ProductSubcategory>
    {
        //controller for handling product subcategories
        //note: methods of adding/updating/deleting subcategories are not included in the client application,
        //since it would not make sense if any user registered in the system would be able to change this information
        //proper logic would be to allow admins to do that, that is why, role admin was put in authorize filter,
        //but it was decided not to develop role based authentication in the application for now,
        //maybe in the future it will be added 
        //that is why, database was already populated with sufficient number of subcategories
        public ProductSubcategoriesController(IRepository<ProductSubcategory> repository):base(repository)
        {
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductSubcategory>>> GetProductSubcategories(int? category)
        {
            var subcategories = await _repository.GetAllAsync();

                var result = subcategories.Where(s => category == null||s.ProductCategoryId== category);
                return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductSubcategory>> GetProductSubcategory(int id)
        {
            var productSubcategory = await _repository.GetByIdAsync(id);

            if (productSubcategory == null)
            {
                return NotFound();
            }

            return productSubcategory;
        }

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles ="Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductSubcategory(int id, ProductSubcategory productSubcategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != productSubcategory.ProductSubcategoryId)
            {
                return BadRequest();
            }

            try
            {
                await _repository.UpdateAsync(productSubcategory);
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

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles ="Admin")]
        [HttpPost]
        public async Task<ActionResult<ProductSubcategory>> PostProductSubcategory(ProductSubcategory productSubcategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _repository.AddAsync(productSubcategory);

            return CreatedAtAction("GetProductSubcategory", new { id = productSubcategory.ProductSubcategoryId }, productSubcategory);
        }

        [Authorize(Roles ="Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductSubcategory(int id)
        {
            var productSubcategory = await _repository.GetByIdAsync(id);
            if (productSubcategory == null)
            {
                return NotFound();
            }

           await _repository.DeleteAsync(productSubcategory);

            return NoContent();
        }

    }
}
