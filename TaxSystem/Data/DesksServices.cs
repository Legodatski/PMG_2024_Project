using System.ComponentModel.DataAnnotations.Schema;

namespace TaxSystem.Data
{
    public class DeskAmenity
    {
        [ForeignKey(nameof(Desk))]
        public int DeskId { get; set; }

        [ForeignKey(nameof(Amenity))]
        public int ServiceId { get; set; }

        public Desk Desk { get; set; }

        public Amenity Amenity { get; set; }
    }
}
