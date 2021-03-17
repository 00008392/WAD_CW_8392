using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WAD._8392.DAL.Context;
using WAD._8392.DAL.DBO;
using WAD._8392.DAL.Repositories;
using WAD._8392.WebApp.Filters;
using WAD._8392.WebApp.Mappings;
using WAD._8392.WebApp.QueryParameters;

namespace WAD._8392.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : GenericController<Product>
    {
        private readonly IStrategyFactory _factory;

        public ProductsController(IRepository<Product> repository, IStrategyFactory factory):base(repository)
        {
            _factory = factory;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts([FromQuery] FilterQueryParameter parameter)
        {
           var products = await _repository.GetAllAsync();
            if(parameter.Name==null)
            {
                return products;
            } else
            {
                var strategy = _factory.GetStrategy(parameter.Name);
                if (strategy == null)
                {
                    ModelState.AddModelError("", "Invalid parameter name");
                    return BadRequest(ModelState);
                }
                var context = new FilterContext();
                context.SetStrategy(strategy);
                var result = context.ExecuteFiltering(parameter.Value, products);
                return Ok(result);
            }
       

        }

        // GET: api/Products/5
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _repository.GetByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.ProductId)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            

            try
            {
                await _repository.UpdateAsync(product);
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

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            product.UserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            product.DatePublished = DateTime.Now;
            product.Status = Status.Available;
            
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _repository.AddAsync(product);

            return CreatedAtAction("GetProduct", new { id = product.ProductId }, product);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            await _repository.DeleteAsync(product);

            return NoContent();
        }


        
    }
}
