using System.ComponentModel.DataAnnotations;
using TaxSystem.Extensions;

namespace TaxSystem.Models.DeskModels
{
    public class AddDeskServiceViewModel
    {
        public AddDeskServiceViewModel()
        {
            AllServiceNames = new HashSet<string>();
        }

        [Required(ErrorMessage = GlobalConstants.RequiredErrorMsg)]
        public int DeskId { get; set; }

        [Display(Name = "Название на услугата")]
        public string ServiceName { get; set; }

        [Required(ErrorMessage = GlobalConstants.RequiredErrorMsg)]
        public IEnumerable<string> AllServiceNames { get; set; }
    }
}
