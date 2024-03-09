using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace TaxSystem.Data
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
    }
}
