using System.ComponentModel.DataAnnotations;

namespace TaxSystem.Models.Service
{
    public class ServiceViewModel
    {
        public ServiceViewModel()
        {
            DeskIds = new List<string>();
            WorkerNames = new List<string>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public double RequiredHours { get; set; }

        public List<string> DeskIds { get; set; }

        public List<string> WorkerNames { get; set; }
    }
}
