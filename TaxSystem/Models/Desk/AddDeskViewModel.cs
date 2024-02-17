using TaxSystem.Data;

namespace TaxSystem.Models.Desk
{
    public class AddDeskViewModel
    {
        public string WorkerId { get; set; }

        public ICollection<ApplicationUser> AllWorkers { get; set; }
    }
}
