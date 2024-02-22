
using System.ComponentModel.DataAnnotations;
using TaxSystem.Data;

namespace TaxSystem.Models.Requests
{
    public class AddRequestViewModel
    {
        public AddRequestViewModel()
        {
            Services = new HashSet<string>();
        }

        public ApplicationUser? User { get; set; }

        [Required]
        public string ServiceName { get; set; }

        public IEnumerable<string> Services { get; set; }

    }
}
