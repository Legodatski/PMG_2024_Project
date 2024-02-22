using System.ComponentModel.DataAnnotations;

namespace TaxSystem.Models.Service
{
    public class ServiceViewModel
    {
        public ServiceViewModel()
        {
            DeskIds = new List<string>();
            WorkerFirstNames = new List<string>();
            WorkerLastNames = new List<string>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string RequiredMinutes { get; set; }

        public List<string> DeskIds { get; set; }

        public List<string> WorkerFirstNames { get; set; }
        public List<string> WorkerLastNames { get; set; }
    }
}
