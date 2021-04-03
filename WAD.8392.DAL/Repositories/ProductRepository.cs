using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WAD._8392.DAL.Context;
using WAD._8392.DAL.DBO;

namespace WAD._8392.DAL.Repositories
{
    public class ProductRepository : BaseRepository, IRepository<Product>
    {
        public ProductRepository(MusicInstrumentsDbContext context):base(context)
        {
        }
        public async Task AddAsync(Product value)
        {
            _context.Products.Add(value);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Product value)
        {
            _context.Products.Remove(value);
            await _context.SaveChangesAsync();
        }
        public async Task<List<Product>> GetAllAsync()
        {
            //display information from related tables as well
            return await _context.Products.Include("Manufacturer").Include("Owner").Include("ProductSubcategory").Include("ProductSubcategory.ProductCategory").ToListAsync();
        }
        public async Task<Product> GetByIdAsync(int id)
        {
            var product = await _context.Products.Include("Manufacturer").Include("Owner").Include("ProductSubcategory").Include("ProductSubcategory.ProductCategory").FirstOrDefaultAsync(p=>p.ProductId==id);
            return product;
        }
        public bool IfExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
        public async Task UpdateAsync(Product value)
        {
            _context.Entry(value).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
