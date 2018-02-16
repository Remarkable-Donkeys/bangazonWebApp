using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace bangazonWebApp.Models
{
    public class Recommendations
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Recommender")]
        public ApplicationUser Recommender { get; set; }

        [Required]
        [Display(Name = "Recommendee")]
        public ApplicationUser Recommendee { get; set; }

        [Required]
        [Display(Name = "Product")]
        public int ProductId { get; set; }

        public Product Product { get; set; }
    }
}
