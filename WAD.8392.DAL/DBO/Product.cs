using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;



namespace WAD._8392.DAL.DBO
{
    //music instrument
    public class Product
    {
        public int ProductId { get; set; }
        [Required(ErrorMessage ="Product name is required")]
        [MinLength(3, ErrorMessage ="Product name should have at least 3 characters")]
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        //convert enum values to display string instead of int in json
        [JsonConverter(typeof(JsonStringEnumConverter))]
        [Required(ErrorMessage ="Condition is required")]
        public Condition Condition { get; set; }
        [Required(ErrorMessage ="Price is required")]
        [Range(0, double.MaxValue, ErrorMessage ="Invalid price")]
        public decimal Price { get; set; }
        //convert enum values to display string instead of int in json
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Status Status { get; set; } 
        [Required(ErrorMessage ="Location is required")]
        public string Location { get; set; }
        public DateTime DatePublished { get; set; } 
        public int ManufacturerId { get; set; }
        public int ProductSubcategoryId { get; set; }
        public int UserId { get; set; }
        public virtual Manufacturer Manufacturer { get; set; }
        public virtual ProductSubcategory ProductSubcategory { get; set; }
        public virtual User Owner { get; set; }
    }

    public enum Condition
    {
        //enum member - value to be displayed in json
        [EnumMember(Value = "New")]
        New,
        [EnumMember(Value = "Medium")]
        Medium,
        [EnumMember(Value = "Old")]
        Old
    }
    public enum Status
    {
        [EnumMember(Value = "Available")]
        Available,
        [EnumMember(Value = "Booked")]
        Booked,
        [EnumMember(Value = "Sold")]
        Sold
    }
}
