using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WAD._8392.WebApp.Filters
{
    public class StrategyFactory : IStrategyFactory
    {
        public IProductFilter GetStrategy(string parameter)
        {
            switch (parameter)
            {
                case "user":
                    return new UserProductFilter();
                case "manufacturer":
                    return new ManufacturerProductFilter();
                case "subcategory":
                    return new SubcategoryProductFilter();
                default:
                    return null;
            }
        }
    }
}
