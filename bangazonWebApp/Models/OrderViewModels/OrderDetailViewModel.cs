using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bangazonWebApp.Models.OrderViewModels
{
    public class OrderDetailViewModel
    {
        public List<OrderProduct> ProductList { get; set; }
        public Order Order { get; set; }
    }
}
