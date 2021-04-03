using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WAD._8392.DAL.DBO
{
    public class ProductCategory
    {
        public int ProductCategoryId { get; set; }
        [Required(ErrorMessage ="Category name is required")]
        [MinLength(3, ErrorMessage ="Category name should have at least 3 characters")]
        public string CategoryName { get; set; }
        [JsonIgnore]
        public virtual ICollection<ProductSubcategory> ProductSubcategories { get; set; }
    }
}
