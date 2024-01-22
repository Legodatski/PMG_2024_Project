using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaxSystem.Data
{
    public class Request
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public ApplicationUser Client { get; set; }

        [Required]
        [ForeignKey(nameof(Client))]
        public string ClientId { get; set; }

        [Required]
        public Service Service { get; set; }

        [Required]
        [ForeignKey(nameof(Service))]
        public int ServiceId { get; set; }

        [Required]
        public Desk Desk { get; set; }

        [Required]
        [ForeignKey(nameof(Desk))]
        public int DeskId { get; set; }

        [Required]
        public bool IsDeleted { get; set; }
    }
}
