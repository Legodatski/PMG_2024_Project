using System.ComponentModel.DataAnnotations;
using TaxSystem.Extensions;

namespace TaxSystem.Models.DeskModels
{
    public class RemoveDeskServiceViewModel
    {
        public RemoveDeskServiceViewModel()
        {
            ServicesNames = new HashSet<string>();
        }

        [Required(ErrorMessage = GlobalConstants.RequiredErrorMsg)]
        public int DeskId { get; set; }

        [Required(ErrorMessage = GlobalConstants.RequiredErrorMsg)]
        [Display(Name = "Название на услугата")]
        public string ServiceName { get; set;}

        public IEnumerable<string> ServicesNames { get; set; }
    }
}
