using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WAD._8392.WebApp.QueryParameters
{
    public class FilterQueryParameter
    {
        public int? Manufacturer { get; set; }
        public int? User { get; set; }
        public int? Subcategory { get; set; }
        public int? Status { get; set; }
        public int? Category { get; set; }
    }
}
