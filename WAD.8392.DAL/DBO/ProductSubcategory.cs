using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WAD._8392.DAL.DBO
{
    public class ProductSubcategory
    {
        public int ProductSubcategoryId { get; set; }
        [Required]
        [MinLength(3)]
        public string SubcategoryName { get; set; }
        public int? ProductCategoryId { get; set; }
        public virtual ProductCategory ProductCategory { get; set; }
        [JsonIgnore]
        public virtual ICollection<Product> Products { get; set; }

    }
}
