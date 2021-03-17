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
    public class UserRepository : BaseRepository, IRepository<User>
    {

        public UserRepository(MusicInstrumentsDbContext context):base(context)
        {

        }

        public async Task AddAsync(User value)
        {
            
            _context.Users.Add(value);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(User value)
        {
            _context.Users.Remove(value);
            await _context.SaveChangesAsync();
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _context.Users.OrderByDescending(user=>user.DateRegistered).ToListAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public bool IfExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }

        public async Task UpdateAsync(User value)
        {
            _context.Entry(value).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

    }
}
