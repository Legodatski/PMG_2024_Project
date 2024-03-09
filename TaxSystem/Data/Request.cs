using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaxSystem.Data
{
    public class Request
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public ApplicationUser Client { get; set; }

        [Required]
        [ForeignKey(nameof(Client))]
        public string ClientId { get; set; }

        [Required]
        public Amenity Amenity { get; set; }

        [Required]
        [ForeignKey(nameof(Amenity))]
        public int AmenityId { get; set; }

        [Required]
        public string Time { get; set; }

        [Required]
        public Desk Desk { get; set; }

        [Required]
        [ForeignKey(nameof(Desk))]
        public int DeskId { get; set; }

        [Required]
        public bool IsCompleted { get; set; }
    }
}
