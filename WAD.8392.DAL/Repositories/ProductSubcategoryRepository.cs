﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WAD._8392.DAL.Context;
using WAD._8392.DAL.DBO;

namespace WAD._8392.DAL.Repositories
{
    public class ProductSubcategoryRepository : IRepository<ProductSubcategory>
    {
        private readonly MusicInstrumentsDbContext _context;
        public ProductSubcategoryRepository(MusicInstrumentsDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(ProductSubcategory value)
        {
            _context.ProductSubcategories.Add(value);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(ProductSubcategory value)
        {
            _context.ProductSubcategories.Remove(value);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ProductSubcategory>> GetAllAsync()
        {
            return await _context.ProductSubcategories.ToListAsync();
        }

        public async Task<ProductSubcategory> GetByIdAsync(int id)
        {
            return await _context.ProductSubcategories.FindAsync(id);

        }

        public bool IfExists(int id)
        {
            return _context.ProductSubcategories.Any(e => e.ProductSubcategoryId == id);
        }

        public async Task UpdateAsync(ProductSubcategory value)
        {
            _context.Entry(value).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
