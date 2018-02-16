﻿// Author: Jason Figueroa
// Description: This is the model for the Recommendation Table & Controller

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace bangazonWebApp.Models
{
    public class Recommendation
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