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
using WAD._8392.WebApp.QueryParameters;

namespace WAD._8392.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : GenericController<Product>
    {
        //controller for products manipulation
        public ProductsController(IRepository<Product> repository):base(repository)
        {
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts([FromQuery] FilterQueryParameter parameter)
        {
            //products can be filtered by UserId, ManufacturerId, ProductSubcategoryId and ProductCategoryId
           var products = await _repository.GetAllAsync();
            //if filter parameter is not null, then filter by this parameter
            var result = products.Where(p => (parameter.Manufacturer == null || p.ManufacturerId == parameter.Manufacturer)
            && (parameter.User == null || p.UserId == parameter.User)
            && (parameter.Category==null||p.ProductSubcategory.ProductCategoryId==parameter.Category)
            && (parameter.Subcategory == null || p.ProductSubcategoryId == parameter.Subcategory)
            && (parameter.Status==null || (int)p.Status==parameter.Status)
            )
                //by default product list is returned to the client with the latest products first
            .OrderByDescending(p=>p.DatePublished); 
            return Ok(result);
        }

        // GET: api/Products/5

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {

            //getting particular product by id
            var product = await _repository.GetByIdAsync(id);

            if (product == null)
            {
                return NotFound("Product is not found");
            }

            return product;
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            //modifying product
            //checking if all the fields are valid
            //this check is placed before id check in order to avoid exception if product is null (since automatic model validation is disabled)
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != product.ProductId)
            {
                return BadRequest("Product not found");
            }
            //checking if user is trying to modify his own product (if id of logged user is the same as id of product owner)
            if(!IsAuthorized(product.UserId))
            {
                return Unauthorized("You are not the owner of this product");
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
            //catching the exception in case if user did not indicate valid manufacturer and subcategory of the product or if they are null
            catch (Exception ex)
            {
                var error = CheckForeignKeyConstraintViolation(ex);
                if (error!=null)
                {
                    ModelState.AddModelError("InvalidValues", "Manufacturer or subcategory is not valid");
                    return BadRequest(ModelState);
                }
                throw;
            }

            return NoContent();
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            //checking if all the fields are valid
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //assigning id of logged person as the owner of product
            product.UserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            //automatically assign date of publication when product is created
            product.DatePublished = DateTime.Now;
            //when creating product, status is automatically set to available
            product.Status = Status.Available;
            try
            {
                await _repository.AddAsync(product);
            }
            //catching the exception in case if user did not indicate valid manufacturer and subcategory ids
            //or if userId in product is the id of user that was deleted from database
            catch (Exception ex)
            {
                var error = CheckForeignKeyConstraintViolation(ex);
                if(error!=null)
                {
                    ModelState.AddModelError("Error", error);
                    return BadRequest(ModelState);
                }
                throw;
            }


            return CreatedAtAction("GetProduct", new { id = product.ProductId }, product);
        }

        // DELETE: api/Products/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound("Product does not exist");
            }
            //checking if user is trying to delete his own product (if id of logged user is the same as id of product owner)
            if (!IsAuthorized(product.UserId))
            {
                return Unauthorized("You are not the owner of this product");
            }
            await _repository.DeleteAsync(product);

            return NoContent();
        }

        //check used in methods of modifying/deleting product
        //if user is authorized (has JWT), but id of product that this user is trying to change is different,
        //then user is trying to change not his product
        private bool IsAuthorized(int id)
        {
            //getting the id of logged user
            var loggedUserId = GetLoggedUserId();
            //comparing it with id of entity that user is trying to change
            return loggedUserId == id;

        }

        //method that checks whether foreign key constraints are violated
        private string CheckForeignKeyConstraintViolation(Exception ex)
        {
            //if exception message contains given words, it means that necessary type of exception was caught
            if (ex.InnerException.Message.Contains("FOREIGN KEY constraint"))
            {
                return "Either manufacturer, subcategory or user of the given product are invalid.";
            }
            return null;
        }
        
    }
}
