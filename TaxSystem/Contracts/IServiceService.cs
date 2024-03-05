using TaxSystem.Data;
using TaxSystem.Models.Service;

namespace TaxSystem.Contracts
{
    public interface IServiceService
    {
        public Task Add(Service service);

        public Task<IEnumerable<ServiceViewModel>> GetAll(
            string? searchterm);

        public Task Edit(Service model);

        public IEnumerable<string> GetServiceNames();

        public Task<Service> GetService(int id);

        public Task Delete(int id);

        public Task<IEnumerable<string>> GetServiceNamesExcludingDesk(int deskId);

        public Task<IEnumerable<string>> GetServiceNamesByDesk(int deskId);
    }
}
