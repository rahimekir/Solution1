using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Web.Identity;

namespace Web.Models
{
    public class RoleModel
    {
        [Required]
        public string Name { get; set; }
    }
}


    