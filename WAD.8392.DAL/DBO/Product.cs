using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WAD._8392.DAL.DBO
{
    public class Product
    {
        public int ProductId { get; set; }
        [Required]
        [MinLength(3)]
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public Condition Condition { get; set; }
        public decimal Price { get; set; }
        public Status Status { get; set; } 
        public string Location { get; set; }
        public DateTime DatePublished { get; set; } 
        public int? ManufacturerId { get; set; }
        public int? ProductSubcategoryId { get; set; }
        public int? UserId { get; set; }
        public virtual Manufacturer Manufacturer { get; set; }
        public virtual ProductSubcategory ProductSubcategory { get; set; }
        public virtual User Owner { get; set; }
    }

    public enum Condition
    {
        New,
        Medium,
        Old
    }
    public enum Status
    {
        Available,
        Booked,
        Sold
    }
}
