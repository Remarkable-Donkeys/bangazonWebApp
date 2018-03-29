using System.ComponentModel.DataAnnotations;
using bangazonWebApp.Models;
/*
* Author: Greg Turner
* Purpose: Defines the OrderProduct Model/JoinerTable Properties/Columns
*/

namespace bangazonWebApp.Models
{
    public class OrderProduct
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int OrderId { get; set; }
        public Order Order { get; set; }

        [Required]
        public int ProductId { get; set; }
        public Product Product { get; set; }
        
    }
}