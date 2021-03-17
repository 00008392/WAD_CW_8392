using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WAD._8392.DAL.DBO;

namespace WAD._8392.WebApp.Filters
{
    public class FilterContext
    {
        private  IProductFilter _strategy;
        public void SetStrategy(IProductFilter strategy)
        {
            _strategy = strategy;
        }
        public IEnumerable<Product> ExecuteFiltering(int id, IEnumerable<Product> products)
        {
            return _strategy.Filter(id, products);
        }
    }
}
