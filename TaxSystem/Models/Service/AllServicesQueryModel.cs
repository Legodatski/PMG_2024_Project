namespace TaxSystem.Models.Service
{
    public class AllServicesQueryModel
    {
        public AllServicesQueryModel()
        {
            Services = new HashSet<ServiceViewModel>();
        }
        public string SearchTerm { get; set; } = null;

        public IEnumerable<ServiceViewModel> Services { get; set; }
    }
}
