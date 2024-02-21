
using System.ComponentModel.DataAnnotations;

namespace TaxSystem.Models.Requests
{
    public class AddRequestViewModel
    {
        public AddRequestViewModel()
        {
            Services = new HashSet<string>();
        }

        public string UserId { get; set; }

        [Required]
        public string ServiceName { get; set; }

        public IEnumerable<string> Services { get; set; }

    }
}
