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
    [Route("api/categories")]
    [ApiController]
    public class ProductCategoriesController : GenericController<ProductCategory>
    {
        //controller for handling product categories
        //note: methods of adding/updating/deleting categories are not included in the client application,
        //since it would not make sense if any user registered in the system would be able to change this information
        //proper logic would be to allow admins to do that, that is why, role admin was put in authorize filter,
        //but it was decided not to develop role based authentication in the application for now,
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
        [Authorize(Roles ="Admin")]
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
        [Authorize(Roles ="Admin")]
        [HttpPost]
        public async Task<ActionResult<ProductCategory>> PostProductCategory(ProductCategory productCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _repository.AddAsync(productCategory);

            return CreatedAtAction("GetProductCategory", new { id = productCategory.ProductCategoryId }, productCategory);
        }
        [Authorize(Roles ="Admin")]
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
