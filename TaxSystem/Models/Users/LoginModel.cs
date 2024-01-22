using System.ComponentModel.DataAnnotations;
using TaxSystem.Extensions;

namespace TaxSystem.Models.Users
{
    public class LoginModel
    {
        [Required(ErrorMessage = GlobalConstants.RequiredErrorMsg)]
        [EmailAddress]
        [Display(Name = "Email")]
        public string? Email { get; set; }

        [Required(ErrorMessage = GlobalConstants.RequiredErrorMsg)]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
