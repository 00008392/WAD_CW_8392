using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WAD._8392.WebApp.QueryParameters
{
    //parameters necessary for filtering list of products, passed through query string
    public class FilterQueryParameter
    {
        //filter products by ManufacturerId
        public int? Manufacturer { get; set; }
        //filter products by UserId
        public int? User { get; set; }
        //filter products by SubcategoryId
        public int? Subcategory { get; set; }
        //filter product by Status value
        public int? Status { get; set; }
        //filter product by categoryId (linked through subcategory)
        public int? Category { get; set; }
    }
}
