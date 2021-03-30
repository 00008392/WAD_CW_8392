﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WAD._8392.WebApp.Conversion
{
   public interface IConverter<O, D> where O: class
                                  where D: class
    {
        D ConvertToDTO(O obj);
    }
}
