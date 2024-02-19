using System.ComponentModel.DataAnnotations;
using TaxSystem.Data;

namespace TaxSystem.Models.DeskModels
{
    public class AddDeskViewModel
    {
        public AddDeskViewModel()
        {
            AllWorkers = new HashSet<ApplicationUser>();
        }

        [Required]
        public string WorkerId { get; set; }

        public ICollection<ApplicationUser> AllWorkers { get; set; }
    }
}
