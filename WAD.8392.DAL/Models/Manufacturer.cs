using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WAD._8392.DAL.Models
{
    public class Manufacturer
    {
        public int ManufacturerId { get; set; }
        [Required]
        [MinLength(3)]
        public string ManufacturerName { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
