using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaxSystem.Data
{
    public class Desk
    {
        public Desk()
        {
            Requests = new List<Request>();
            Amenities = new List<DeskAmenity>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(Worker))]
        public string WorkerId { get; set; }

        [Required]
        public ApplicationUser Worker { get; set; }

        public ICollection<DeskAmenity> Amenities { get; set; }

        public ICollection<Request> Requests { get; set; }

    }
}
