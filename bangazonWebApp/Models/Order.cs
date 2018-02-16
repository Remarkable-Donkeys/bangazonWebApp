using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using bangazonWebApp.Models;
/*
* Author: Greg Turner
* Purpose: Defines the Order Model/Table Properties/Columns
*/

namespace Bangazon.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public ApplicationUser User { get; set; }

        public int PaymentId { get; set; }
        public PaymentType PaymentType { get; set; }
        

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateCreated { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateClosed { get; set; }

        public virtual ICollection<OrderProduct> OrderProduct { get; set; }
    }
}