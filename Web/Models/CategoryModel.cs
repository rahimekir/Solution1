using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class CategoryModel
    {
        public int CategoryId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "The description field should be between 5-10000.")]


        public string Url { get; set; }
       
        public List<Product> Products { get; set; }
    }
    
}
