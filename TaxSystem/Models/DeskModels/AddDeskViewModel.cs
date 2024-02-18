using System.ComponentModel.DataAnnotations;
using TaxSystem.Data;

namespace TaxSystem.Models.DeskModels
{
    public class AddDeskViewModel
    {
        [Required]
        public string WorkerId { get; set; }

        public ICollection<ApplicationUser> AllWorkers { get; set; }
    }
}
