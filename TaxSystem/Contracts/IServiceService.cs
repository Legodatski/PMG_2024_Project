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
    }
}
