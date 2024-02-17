namespace TaxSystem.Models.Requests
{
    public class AddRequestViewModel
    {
        public AddRequestViewModel()
        {
            Services = new HashSet<Data.Service>();
        }

        public string UserId { get; set; }

        public int ServiceId { get; set; }

        public IEnumerable<Data.Service> Services { get; set; }

    }
}
