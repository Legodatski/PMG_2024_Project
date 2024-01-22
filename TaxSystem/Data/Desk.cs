using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaxSystem.Data
{
    public class Desk
    {
        public Desk()
        {
            Requests = new List<Request>();
            Services = new List<Service>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(Worker))]
        public string WorkerId { get; set; }

        [Required]
        public ApplicationUser Worker { get; set; }

        public ICollection<Service> Services { get; set; }

        public ICollection<Request> Requests { get; set; }

        [Required]
        public bool IsDeleted { get; set; }
    }
}
