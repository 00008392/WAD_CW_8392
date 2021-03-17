using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WAD._8392.WebApp.Filters
{
  public interface IStrategyFactory
    {
        IProductFilter GetStrategy(string parameter);
    }
}
