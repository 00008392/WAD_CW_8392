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
    public class ManufacturerRepository : BaseRepository, IRepository<Manufacturer>
    {
        public ManufacturerRepository(MusicInstrumentsDbContext context):base(context)
        {
        }
        public async Task AddAsync(Manufacturer value)
        {
            _context.Manufacturers.Add(value);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Manufacturer value)
        {
            _context.Manufacturers.Remove(value);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Manufacturer>> GetAllAsync()
        {
            return await _context.Manufacturers.ToListAsync();
        }

        public async Task<Manufacturer> GetByIdAsync(int id)
        {
            return await _context.Manufacturers.FindAsync(id);
        }

        public bool IfExists(int id)
        {
            return _context.Manufacturers.Any(e => e.ManufacturerId == id);
        }

        public async Task UpdateAsync(Manufacturer value)
        {
            _context.Entry(value).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
