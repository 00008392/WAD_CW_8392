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
    [Route("api/[controller]")]
    [ApiController]
    public class ProductSubcategoriesController : ControllerBase
    {
        private readonly IRepository<ProductSubcategory> _productSubcategoryRepository;

        public ProductSubcategoriesController(IRepository<ProductSubcategory> productSubcategoryRepository)
        {
            _productSubcategoryRepository = productSubcategoryRepository;
        }

        // GET: api/ProductSubcategories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductSubcategory>>> GetProductSubcategories()
        {
            return await _productSubcategoryRepository.GetAllAsync();
        }

        // GET: api/ProductSubcategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductSubcategory>> GetProductSubcategory(int id)
        {
            var productSubcategory = await _productSubcategoryRepository.GetByIdAsync(id);

            if (productSubcategory == null)
            {
                return NotFound();
            }

            return productSubcategory;
        }

        // PUT: api/ProductSubcategories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
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
                await _productSubcategoryRepository.UpdateAsync(productSubcategory);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductSubcategoryExists(id))
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

        // POST: api/ProductSubcategories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductSubcategory>> PostProductSubcategory(ProductSubcategory productSubcategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _productSubcategoryRepository.AddAsync(productSubcategory);

            return CreatedAtAction("GetProductSubcategory", new { id = productSubcategory.ProductSubcategoryId }, productSubcategory);
        }

        // DELETE: api/ProductSubcategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductSubcategory(int id)
        {
            var productSubcategory = await _productSubcategoryRepository.GetByIdAsync(id);
            if (productSubcategory == null)
            {
                return NotFound();
            }

           await _productSubcategoryRepository.DeleteAsync(productSubcategory);

            return NoContent();
        }

        private bool ProductSubcategoryExists(int id)
        {
            return _productSubcategoryRepository.IfExists(id);
        }
    }
}
