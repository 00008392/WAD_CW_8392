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
    public class Product
    {
        public int ProductId { get; set; }
        [Required]
        [MinLength(3)]
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        [Required]
        public Condition Condition { get; set; }
        [Required]
        public decimal Price { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Status Status { get; set; } 
        [Required]
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
