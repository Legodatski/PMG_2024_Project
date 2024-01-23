using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using TaxSystem.Extensions;

namespace TaxSystem.Models.User
{
    public class EditUserModel
    {
        public EditUserModel()
        {
            Roles = new HashSet<IdentityRole>();
        }

        [Required]
        public string Id { get; set; }

        [Required(ErrorMessage = GlobalConstants.RequiredErrorMsg)]
        public string? Username { get; set; }

        [Required(ErrorMessage = GlobalConstants.RequiredErrorMsg)]
        [EmailAddress]
        [Display(Name = "Email")]
        public string? Email { get; set; }

        [Required(ErrorMessage = GlobalConstants.RequiredErrorMsg)]
        [Display(Name = "First Name")]
        [MinLength(GlobalConstants.FirstNameMinLenght)]
        [MaxLength(GlobalConstants.FirstNameMaxLenght)]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = GlobalConstants.RequiredErrorMsg)]
        [Display(Name = "Last Name")]
        [MinLength(GlobalConstants.LastNameMinLenght)]
        [MaxLength(GlobalConstants.LastNameMaxLenght)]
        public string? LastName { get; set; }

        [Required(ErrorMessage = GlobalConstants.RequiredErrorMsg)]
        [Display(Name = "Phone Number")]
        public string? PhoneNumber { get; set; }

        public string RoleName { get; set; }

        public required IEnumerable<IdentityRole> Roles { get; set; }

    }
}
