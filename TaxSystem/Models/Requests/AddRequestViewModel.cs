
using System.ComponentModel.DataAnnotations;
using TaxSystem.Data;
using TaxSystem.Extensions;

namespace TaxSystem.Models.Requests
{
    public class AddRequestViewModel
    {
        public AddRequestViewModel()
        {
            Services = new HashSet<string>();
        }

        public ApplicationUser? User { get; set; }

        [Required(ErrorMessage = GlobalConstants.RequiredErrorMsg)]
        [Display(Name = "Название на услугата")]
        public string ServiceName { get; set; }

        public IEnumerable<string> Services { get; set; }

    }
}
