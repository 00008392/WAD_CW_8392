using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WAD._8392.DAL.Models
{
    public class ProductSubcategory
    {
        public int ProductSubcategoryId { get; set; }
        [Required]
        [MinLength(3)]
        public string SubcategoryName { get; set; }
        public int? ProductCategoryId { get; set; }
        public virtual ProductCategory ProductCategory { get; set; }
        public virtual ICollection<Product> Products { get; set; }

    }
}
