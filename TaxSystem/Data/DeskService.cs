using System.ComponentModel.DataAnnotations.Schema;

namespace TaxSystem.Data
{
    public class DeskService
    {
        [ForeignKey(nameof(Desk))]
        public int DeskId { get; set; }

        [ForeignKey(nameof(Service))]
        public int ServiceId { get; set; }

        public Desk Desk { get; set; }

        public Service Service { get; set; }
    }
}
