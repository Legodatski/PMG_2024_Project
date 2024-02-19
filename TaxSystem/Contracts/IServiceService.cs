using TaxSystem.Data;
using TaxSystem.Models.Service;

namespace TaxSystem.Contracts
{
    public interface IServiceService
    {
        public Task Add(Service service);

        public Task<IEnumerable<ServiceViewModel>> GetAll(
            string? searchterm,
            int currentPage = 1,
            int usersPerPage = 5);

        public Task Edit(Service model);

        public IEnumerable<string> GetServiceNames();

        public Task<Service> GetService(int id);

        public Task Delete(int id);
    }
}
