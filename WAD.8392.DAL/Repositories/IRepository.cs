using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WAD._8392.DAL.Repositories
{
    public interface IRepository<T> where T: class
    {
        Task AddAsync(T value);
        Task UpdateAsync(T value);
        Task DeleteAsync(T value);
        Task<T> GetByIdAsync(int id);
        Task<List<T>> GetAllAsync();
        bool IfExists(int id);
    }
}
