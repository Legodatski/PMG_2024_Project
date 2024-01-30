using TaxSystem.Data;

namespace TaxSystem.Contracts
{
    public interface IServiceService
    {
        public Task Add(Service service);

        public Task<IEnumerable<Service>> GetAll(
            string? searchterm,
            int currentPage = 1,
            int usersPerPage = 5);
    }
}
