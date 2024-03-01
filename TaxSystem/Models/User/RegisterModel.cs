using System.ComponentModel.DataAnnotations;
using TaxSystem.Extensions;

namespace TaxSystem.Models.User
{
    public class RegisterModel
    {
        [Required(ErrorMessage = GlobalConstants.RequiredErrorMsg)]
        [Display(Name = "Потребителско име")]
        public string? Username { get; set; }

        [Required(ErrorMessage = GlobalConstants.RequiredErrorMsg)]
        [EmailAddress]
        [Display(Name = "Имейл")]
        public string? Email { get; set; }

        [Required(ErrorMessage = GlobalConstants.RequiredErrorMsg)]
        [Display(Name = "Първо име")]
        [MinLength(GlobalConstants.FirstNameMinLenght)]
        [MaxLength(GlobalConstants.FirstNameMaxLenght)]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = GlobalConstants.RequiredErrorMsg)]
        [Display(Name = "Фамилия")]
        [MinLength(GlobalConstants.LastNameMinLenght)]
        [MaxLength(GlobalConstants.LastNameMaxLenght)]
        public string? LastName { get; set; }

        [Required(ErrorMessage = GlobalConstants.RequiredErrorMsg)]
        [DataType(DataType.Password)]
        [MinLength(GlobalConstants.PasswordMinLenght)]
        [MaxLength(GlobalConstants.PasswordMaxLenght)]
        [Display(Name = "Парола")]
        public string? Password { get; set; }

        [Required(ErrorMessage = GlobalConstants.RequiredErrorMsg)]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        [Display(Name = "Потвърди паролата")]
        public string? ConfirmPassword { get; set; }

        [Required(ErrorMessage = GlobalConstants.RequiredErrorMsg)]
        [Display(Name = "Телефонен номер")]
        public string? PhoneNumber { get; set; }
    }
}
