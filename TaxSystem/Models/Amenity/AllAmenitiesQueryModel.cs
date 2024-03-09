namespace TaxSystem.Models.Amenity
{
    public class AllAmenitiesQueryModel
    {
        public AllAmenitiesQueryModel()
        {
            Services = new HashSet<AmenityViewModel>();
        }
        public string SearchTerm { get; set; } = null;

        public IEnumerable<AmenityViewModel> Services { get; set; }
    }
}
