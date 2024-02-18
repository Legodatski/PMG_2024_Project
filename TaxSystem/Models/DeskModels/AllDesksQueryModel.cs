using TaxSystem.Data;

namespace TaxSystem.Models.DeskModels
{
    public class AllDesksQueryModel
    {
        public AllDesksQueryModel()
        {
            Desks = new HashSet<Desk>();
        }

        public int DesksPerPage { get; set; } = 5;

        public int CurrentPage { get; set; } = 1;

        public string? SearchTerm { get; set; }

        public IEnumerable<Desk> Desks { get; set; }
    }
}
