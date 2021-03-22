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
    public class ProductSubcategoriesController : GenericController<ProductSubcategory>
    {

        public ProductSubcategoriesController(IRepository<ProductSubcategory> repository):base(repository)
        {
        }
        [HttpGet("subcategories")]
        public async Task<ActionResult<IEnumerable<ProductSubcategory>>> GetProductSubcategories(int? id)
        {
            var subcategories = await _repository.GetAllAsync();

                var result = subcategories.Where(s => id==null||s.ProductCategoryId==id);
                return Ok(result);
        }

        [HttpGet("subcategories/{id}")]
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
        [Authorize]
        [HttpPut("subcategories/{id}")]
        public async Task<IActionResult> PutProductSubcategory(int id, ProductSubcategory productSubcategory)
        {
            if (id != productSubcategory.ProductSubcategoryId)
            {
                return BadRequest();
            }
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
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
        [Authorize]
        [HttpPost("subcategories")]
        public async Task<ActionResult<ProductSubcategory>> PostProductSubcategory(ProductSubcategory productSubcategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _repository.AddAsync(productSubcategory);

            return CreatedAtAction("GetProductSubcategory", new { id = productSubcategory.ProductSubcategoryId }, productSubcategory);
        }

        [Authorize]
        [HttpDelete("subcategories/{id}")]
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
