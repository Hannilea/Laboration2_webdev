using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;


namespace TownSquareAuth.Models
{
    public class ApplicationUser : IdentityUser
    {

        public string? FullName { get; set; } = string.Empty;
    }
}