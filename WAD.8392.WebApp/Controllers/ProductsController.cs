﻿using System;
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
using WAD._8392.WebApp.Mappings;

namespace WAD._8392.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : GenericController<Product>
    {
        private readonly UsernameMapper _mapper;

        public ProductsController(IRepository<Product> repository, UsernameMapper mapper):base(repository)
        {
            _mapper = mapper;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            
            return await _repository.GetAllAsync();
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
            var owner = await _mapper.MapUsernameToUser(User.Identity.Name);
            product.UserId = owner.UserId;
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
