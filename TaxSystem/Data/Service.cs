using System.ComponentModel.DataAnnotations;

namespace TaxSystem.Data
{
    public class Service
    {
        public Service()
        {
            Desks = new List<Desk>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public double RequiredHours { get; set; }

        public ICollection<Desk> Desks { get; set; }

    }
}
