using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WAD._8392.DAL.DBO
{
    //music instruments manufacturer
    public class Manufacturer
    {
        public int ManufacturerId { get; set; }
        [Required(ErrorMessage ="Manufacturer name is required")]
        public string ManufacturerName { get; set; }
        [JsonIgnore]
        public virtual ICollection<Product> Products { get; set; }
    }
}
