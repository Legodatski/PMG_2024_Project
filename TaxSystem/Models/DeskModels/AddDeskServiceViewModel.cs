using System.ComponentModel.DataAnnotations;

namespace TaxSystem.Models.DeskModels
{
    public class AddDeskServiceViewModel
    {
        public AddDeskServiceViewModel()
        {
            AllServiceNames = new HashSet<string>();
        }

        [Required]
        public int DeskId { get; set; }

        public string ServiceName { get; set; }

        [Required]
        public IEnumerable<string> AllServiceNames { get; set; }
    }
}
