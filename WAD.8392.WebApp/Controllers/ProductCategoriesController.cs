using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        //controller for handling product categories
        //note: methods of adding/updating/deleting categories are not included in the client application,
        //since it would not make sense if any user registered in the system would be able to change this information
        //logic for these methods is not fully implemented in this controller as well
        //proper logic would be to allow admins to do that, but it was decided not to include this functionality in the application for now,
        //maybe in the future it will be added
        //that is why, database was already populated with sufficient number of categories
        public ProductCategoriesController(IRepository<ProductCategory> repository) : base(repository)
        {
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductCategory>>> GetProductCategories()
        {
            return await _repository.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductCategory>> GetProductCategory(int id)
        {
            var productCategory = await _repository.GetByIdAsync(id);

            if (productCategory == null)
            {
                return NotFound();
            }

            return productCategory;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductCategory(int id, ProductCategory productCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != productCategory.ProductCategoryId)
            {
                return BadRequest();
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

        [HttpPost]
        public async Task<ActionResult<ProductCategory>> PostProductCategory(ProductCategory productCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _repository.AddAsync(productCategory);

            return CreatedAtAction("GetManufacturer", new { id = productCategory.ProductCategoryId }, productCategory);
        }

        [HttpDelete("{id}")]
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
