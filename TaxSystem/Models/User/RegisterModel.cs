using System.ComponentModel.DataAnnotations;
using TaxSystem.Extensions;

namespace TaxSystem.Models.Users
{
    public class RegisterModel
    {
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
        [DataType(DataType.Password)]
        [MinLength(GlobalConstants.PasswordMinLenght)]
        [MaxLength(GlobalConstants.PasswordMaxLenght)]
        public string? Password { get; set; }

        [Required(ErrorMessage = GlobalConstants.RequiredErrorMsg)]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        [Display(Name = "Confirm Password")]
        public string? ConfirmPassword { get; set; }

        [Required(ErrorMessage = GlobalConstants.RequiredErrorMsg)]
        [Display(Name = "Phone Number")]
        public string? PhoneNumber { get; set; }
    }
}
