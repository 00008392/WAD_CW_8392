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
    [Route("api/Categories")]
    [ApiController]
    public class ProductCategoriesController : GenericController<ProductCategory>
    {

        public ProductCategoriesController(IRepository<ProductCategory> repository):base(repository)
        {
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductCategory>>> GetProductCategories()
        {
            return await _repository.GetAllAsync();
        }

        [HttpGet("/{id}")]
        public async Task<ActionResult<ProductCategory>> GetProductCategory(int id)
        {
            var productCategory = await _repository.GetByIdAsync(id);

            if (productCategory == null)
            {
                return NotFound();
            }

            return productCategory;
        }


        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPut("/{id}")]
        public async Task<IActionResult> PutProductCategory(int id, ProductCategory productCategory)
        {
            if (id != productCategory.ProductCategoryId)
            {
                return BadRequest();
            }
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
          

            try
            {
                await _repository.UpdateAsync(productCategory);
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
        [HttpPost]
        public async Task<ActionResult<ProductCategory>> PostProductCategory(ProductCategory productCategory)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _repository.AddAsync(productCategory);

            return CreatedAtAction("GetProductCategory", new { id = productCategory.ProductCategoryId }, productCategory);
        }
        [Authorize]
        [HttpDelete("/{id}")]
        public async Task<IActionResult> DeleteProductCategory(int id)
        {
            var productCategory = await _repository.GetByIdAsync(id);
            if (productCategory == null)
            {
                return NotFound();
            }

            await _repository.DeleteAsync(productCategory);

            return NoContent();
        }

     
    }
}
