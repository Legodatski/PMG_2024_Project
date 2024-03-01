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
        [Display(Name = "Имейл")]
        public string? Email { get; set; }

        [Required(ErrorMessage = GlobalConstants.RequiredErrorMsg)]
        [Display(Name = "Име")]
        [MinLength(GlobalConstants.FirstNameMinLenght)]
        [MaxLength(GlobalConstants.FirstNameMaxLenght)]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = GlobalConstants.RequiredErrorMsg)]
        [Display(Name = "Фамилия")]
        [MinLength(GlobalConstants.LastNameMinLenght)]
        [MaxLength(GlobalConstants.LastNameMaxLenght)]
        public string? LastName { get; set; }

        [Required(ErrorMessage = GlobalConstants.RequiredErrorMsg)]
        [Display(Name = "Телефонен номер")]
        public string? PhoneNumber { get; set; }

        [Display(Name = "Роля")]
        public string RoleName { get; set; }

        public required IEnumerable<IdentityRole> Roles { get; set; }

    }
}
