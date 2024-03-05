using TaxSystem.Data;

namespace TaxSystem.Models.DeskModels
{
    public class AllDesksQueryModel
    {
        public AllDesksQueryModel()
        {
            Desks = new HashSet<Desk>();
        }

        public string? SearchTerm { get; set; }

        public IEnumerable<Desk> Desks { get; set; }
    }
}
