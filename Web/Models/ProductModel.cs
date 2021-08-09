using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class ProductModel
    {
      
            public int ProductId { get; set; }

         
            public string Name { get; set; }

            [Required]
            public string Url { get; set; }

            [Required]
          
            public double? Price { get; set; }

            [Required]
            [StringLength(100, MinimumLength = 5, ErrorMessage = "The description field should be between 5-10000.")]

            public string Description { get; set; }

            [Required]
            public string ImageUrl { get; set; }
            public bool IsApproved { get; set; }
            public bool IsHome { get; set; }
            public List<Category> SelectedCategories { get; set; }
        }
    }


