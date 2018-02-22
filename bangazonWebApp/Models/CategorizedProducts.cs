using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bangazonWebApp.Models
{
    public class CategorizedProducts
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int ProductCount { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
