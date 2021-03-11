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
    public class ProductCategoryRepository : BaseRepository, IRepository<ProductCategory>
    {
        public ProductCategoryRepository(MusicInstrumentsDbContext context):base(context)
        {
        }

        public async Task AddAsync(ProductCategory value)
        {
            _context.ProductCategories.Add(value);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(ProductCategory value)
        {
            _context.ProductCategories.Remove(value);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ProductCategory>> GetAllAsync()
        {
            return await _context.ProductCategories.ToListAsync();
        }

        public async Task<ProductCategory> GetByIdAsync(int id)
        {
            return await _context.ProductCategories.FindAsync(id);
        }

        public bool IfExists(int id)
        {
            return _context.ProductCategories.Any(e => e.ProductCategoryId == id);
        }

        public async Task UpdateAsync(ProductCategory value)
        {
            _context.Entry(value).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
