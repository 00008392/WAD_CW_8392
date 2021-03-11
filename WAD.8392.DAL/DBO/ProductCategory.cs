﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WAD._8392.DAL.DBO
{
    public class ProductCategory
    {
        public int ProductCategoryId { get; set; }
        [Required]
        [MinLength(3)]
        public string CategoryName { get; set; }
        public virtual ICollection<ProductSubcategory> ProductSubcategories { get; set; }
    }
}