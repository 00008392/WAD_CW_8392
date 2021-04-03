using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WAD._8392.DAL.Repositories
{
    //interface for all repositories
    public interface IRepository<T> where T: class
    {
        //add object to database
        Task AddAsync(T value);
        //update object in database
        Task UpdateAsync(T value);
        //delete object from database
        Task DeleteAsync(T value);
        //get particular object from database by id
        Task<T> GetByIdAsync(int id);
        //get list of objects from database
        Task<List<T>> GetAllAsync();
        //check by id if object exists in database
        bool IfExists(int id);
    }
}
