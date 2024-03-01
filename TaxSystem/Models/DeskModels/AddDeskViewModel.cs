using System.ComponentModel.DataAnnotations;
using TaxSystem.Data;
using TaxSystem.Extensions;

namespace TaxSystem.Models.DeskModels
{
    public class AddDeskViewModel
    {
        public AddDeskViewModel()
        {
            AllWorkers = new HashSet<ApplicationUser>();
        }

        [Required(ErrorMessage = GlobalConstants.RequiredErrorMsg)]
        [Display (Name = "Работник")]
        public string WorkerId { get; set; }

        public ICollection<ApplicationUser> AllWorkers { get; set; }
    }
}
