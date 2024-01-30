namespace TaxSystem.Models.Service
{
    public class AllServicesQueryModel
    {
        public AllServicesQueryModel()
        {
            Services = new HashSet<ServiceViewModel>();
        }

        public int UsersPerPage { get; set; } = 5;

        public int CurrentPage { get; set; } = 1;

        public string SearchTerm { get; set; } = null;

        public IEnumerable<ServiceViewModel> Services { get; set; }
    }
}
