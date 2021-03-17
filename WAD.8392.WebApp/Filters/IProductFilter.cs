using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WAD._8392.DAL.DBO;
using WAD._8392.DAL.Repositories;

namespace WAD._8392.WebApp.Filters
{
    public interface IProductFilter
    {
        IEnumerable<Product> Filter(int id, IEnumerable<Product> products);

    }
}
