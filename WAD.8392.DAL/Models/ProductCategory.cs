using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WAD._8392.DAL.Models
{
    public class ProductCategory
    {
        public int CategoryId { get; set; }
        [Required]
        [MinLength(3)]
        public string CategoryName { get; set; }
        public virtual ICollection<ProductSubcategory> ProductSubcategories { get; set; }
    }
}
