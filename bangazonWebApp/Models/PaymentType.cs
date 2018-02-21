//author: Kristen Norris
//purpose: created model for Payment Types

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace bangazonWebApp.Models
{
    public class PaymentType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(12)]
        public string Name { get; set; }

        [Required]
        public string AccountNumber { get; set; }

        [Required]
        public ApplicationUser User { get; set; }

        [Required]
        public bool Active { get; set; } = true;


        public virtual ICollection<Order> Orders { get; set; }

    }
}
